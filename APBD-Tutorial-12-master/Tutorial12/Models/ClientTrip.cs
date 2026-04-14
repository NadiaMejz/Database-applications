using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial12.Models;

[Table("Client_Trip")]
[PrimaryKey(nameof(IdClient), nameof(IdTrip))]
public class ClientTrip
{
    [ForeignKey(nameof(Client))] public int IdClient { get; set; }
    [ForeignKey(nameof(Trip))] public int IdTrip { get; set; }

    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }

    public Client Client { get; set; } = null!;
    public Trip Trip { get; set; } = null!;
}