namespace Tutorial12.DTOs;

public record CountryDto(string Name);

public record ClientDto(string FirstName, string LastName);

public record TripDto(
    string Name,
    string? Description,
    DateTime DateFrom,
    DateTime DateTo,
    int MaxPeople,
    IEnumerable<CountryDto> Countries,
    IEnumerable<ClientDto> Clients);

public record PagedTripsDto(int PageNum, int PageSize, int AllPages, IEnumerable<TripDto> Trips);