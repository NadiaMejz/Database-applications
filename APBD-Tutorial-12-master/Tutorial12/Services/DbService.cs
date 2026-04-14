using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.DTOs;
using Tutorial12.Exceptions;
using Tutorial12.Models;

namespace Tutorial12.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _ctx;

    public DbService(DatabaseContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedTripsDto> GetTrips(int page, int pageSize)
    {
        var query = _ctx.Trips.OrderByDescending(t => t.DateFrom);

        var total = await query.CountAsync();
        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto(
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                t.TripCountries.Select(tc => new CountryDto(tc.Country.Name)),
                t.ClientTrips.Select(ct => new ClientDto(ct.Client.FirstName, ct.Client.LastName))
            ))
            .ToListAsync();

        var pages = (int)Math.Ceiling(total / (double)pageSize);
        return new PagedTripsDto(page, pageSize, pages, trips);
    }

    public async Task DeleteClient(int idClient)
    {
        var client = await _ctx.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
            throw new NotFoundException("Client not found");

        if (client.ClientTrips.Any())
            throw new ConflictException("Client is assigned to at least one trip");

        _ctx.Clients.Remove(client);
        await _ctx.SaveChangesAsync();
    }

    public async Task AssignClientToTrip(int idTrip, CreateClientDto dto)
    {
        var trip = await _ctx.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);
        if (trip == null || trip.DateFrom <= DateTime.Today)
            throw new NotFoundException("Trip not found or already started");

        var client = await _ctx.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);

        if (client == null)
        {
            client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };

            _ctx.Clients.Add(client);
            await _ctx.SaveChangesAsync();
        }

        var exists = await _ctx.ClientTrips
            .AnyAsync(ct => ct.IdTrip == idTrip && ct.IdClient == client.IdClient);

        if (exists)
            throw new ConflictException("Client already assigned to this trip");

        _ctx.ClientTrips.Add(new ClientTrip
        {
            IdTrip = idTrip,
            IdClient = client.IdClient,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = dto.PaymentDate
        });

        await _ctx.SaveChangesAsync();
    }
}