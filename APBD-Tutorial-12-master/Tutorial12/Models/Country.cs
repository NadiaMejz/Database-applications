using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial12.Models;

[Table("Country")]
public class Country
{
    [Key] public int IdCountry { get; set; }

    [MaxLength(50)] public string Name { get; set; } = null!;

    public ICollection<CountryTrip> TripCountries { get; set; } = null!;
}