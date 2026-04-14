using Microsoft.EntityFrameworkCore;
using Tutorial12.Models;

namespace Tutorial12.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<ClientTrip> ClientTrips { get; set; } = null!;
    public DbSet<CountryTrip> TripCountries { get; set; } = null!;

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
}