using APBD25Test2.DTOs;

namespace APBD25Test2.Services;

public interface IDbService
{
    Task<ParticipationsDto> GetParticipationsAsync(int racerId);
    Task AddParticipationsAsync(RaceParticipantCreateDto dto);
}