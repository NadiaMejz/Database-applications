using APBD25Test2.Data;
using APBD25Test2.DTOs;
using APBD25Test2.Exceptions;
using APBD25Test2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD25Test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ParticipationsDto> GetParticipationsAsync(int racerId)
    {
        var racer = await _context.Racers
            .Where(r => r.RacerId == racerId)
            .Select(r => new ParticipationsDto
            {
                RacerId = r.RacerId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Participations = r.RaceParticipations
                    .OrderBy(rp => rp.Position)
                    .Select(rp => new ParticipationDto
                    {
                        Race = new RaceDto
                        {
                            Name = rp.TrackRace.Race.Name,
                            Location = rp.TrackRace.Race.Location,
                            Date = rp.TrackRace.Race.Date
                        },
                        Track = new TrackDto
                        {
                            Name = rp.TrackRace.Track.Name,
                            LengthInKm = rp.TrackRace.Track.LengthInKm
                        },
                        Laps = rp.TrackRace.Laps,
                        FinishTimeInSeconds = rp.FinishTimeInSeconds,
                        Position = rp.Position
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (racer is null)
            throw new NotFoundException("Racer not found.");

        return racer;
    }

    public async Task AddParticipationsAsync(RaceParticipantCreateDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var race = await _context.Races.FirstOrDefaultAsync(r =>
                r.Name.Equals(dto.RaceName));
            if (race is null)
                throw new NotFoundException("Race with given name does not exist.");

            var track = await _context.Tracks.FirstOrDefaultAsync(t =>
                t.Name.Equals(dto.TrackName));
            if (track is null)
                throw new NotFoundException("Track with given name does not exist.");

            var trackRace = await _context.TrackRaces
                .FirstOrDefaultAsync(tr => tr.TrackId == track.TrackId &&
                                           tr.RaceId == race.RaceId);
            if (trackRace is null)
                throw new NotFoundException("Given race is not on that track.");

            foreach (var p in dto.Participations)
            {
                var racerExists = await _context.Racers.AnyAsync(r => r.RacerId == p.RacerId);
                if (!racerExists)
                    throw new NotFoundException($"Racer with id {p.RacerId} not found.");

                var existing = await _context.RaceParticipations
                    .FirstOrDefaultAsync(rp => rp.TrackRaceId == trackRace.TrackRaceId &&
                                               rp.RacerId == p.RacerId);

                if (existing is null)
                {
                    _context.RaceParticipations.Add(new RaceParticipation
                    {
                        TrackRaceId = trackRace.TrackRaceId,
                        RacerId = p.RacerId,
                        FinishTimeInSeconds = p.FinishTimeInSeconds,
                        Position = p.Position
                    });
                }
                else
                {
                    existing.FinishTimeInSeconds = p.FinishTimeInSeconds;
                    existing.Position = p.Position;
                }

                if (trackRace.BestTimeInSeconds is null ||
                    p.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
                {
                    trackRace.BestTimeInSeconds = p.FinishTimeInSeconds;
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}