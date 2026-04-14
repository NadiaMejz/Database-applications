using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial12.Models;

[Table("Trip")]
public class Trip
{
    [Key] public int IdTrip { get; set; }

    [MaxLength(100)] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }

    public ICollection<ClientTrip> ClientTrips { get; set; } = null!;
    public ICollection<CountryTrip> TripCountries { get; set; } = null!;
}