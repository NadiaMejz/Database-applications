using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial12.Models;

[Table("Country_Trip")]
[PrimaryKey(nameof(IdTrip), nameof(IdCountry))]
public class CountryTrip
{
    [ForeignKey(nameof(Trip))] public int IdTrip { get; set; }
    [ForeignKey(nameof(Country))] public int IdCountry { get; set; }

    public Trip Trip { get; set; } = null!;
    public Country Country { get; set; } = null!;
}