namespace APBD25Test2.DTOs;

public class RaceParticipantCreateDto
{
    public string RaceName { get; set; } = null!;
    public string TrackName { get; set; } = null!;
    public List<ParticipationCreateDto> Participations { get; set; } = null!;
}

public class ParticipationCreateDto
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishTimeInSeconds { get; set; }
}