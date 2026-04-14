using Tutorial12.DTOs;

namespace Tutorial12.Services;

public interface IDbService
{
    Task<PagedTripsDto> GetTrips(int page, int pageSize);
    Task DeleteClient(int idClient);
    Task AssignClientToTrip(int idTrip, CreateClientDto dto);
}