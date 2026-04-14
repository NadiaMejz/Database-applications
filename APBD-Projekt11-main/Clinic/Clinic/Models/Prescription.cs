using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models;

[Table("Prescription")]
public class Prescription
{
    [Key] public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

    public int IdPatient { get; set; }
    [ForeignKey(nameof(IdPatient))] public Patient Patient { get; set; } = null!;

    public int IdDoctor { get; set; }
    [ForeignKey(nameof(IdDoctor))] public Doctor Doctor { get; set; } = null!;

    public ICollection<PrescriptionMedicament> MedicamentLinks { get; set; } = new HashSet<PrescriptionMedicament>();
}