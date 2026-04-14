using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models;

[Table("Medicament")]
public class Medicament
{
    [Key] public int IdMedicament { get; set; }
    [MaxLength(100)] public string Name { get; set; } = null!;
    [MaxLength(100)] public string Description { get; set; } = null!;
    [MaxLength(100)] public string Type { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionLinks { get; set; } = new HashSet<PrescriptionMedicament>();
}
