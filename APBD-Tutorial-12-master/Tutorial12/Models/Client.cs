using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial12.Models;

[Table("Client")]
public class Client
{
    [Key] public int IdClient { get; set; }

    [MaxLength(50)] public string FirstName { get; set; } = null!;
    [MaxLength(50)] public string LastName { get; set; } = null!;
    [MaxLength(100)] public string Email { get; set; } = null!;
    [MaxLength(20)] public string Telephone { get; set; } = null!;
    [MaxLength(11)] public string Pesel { get; set; } = null!;

    public ICollection<ClientTrip> ClientTrips { get; set; } = null!;
}