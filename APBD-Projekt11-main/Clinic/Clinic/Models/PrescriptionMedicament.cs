using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdPrescription), nameof(IdMedicament))]
public class PrescriptionMedicament
{
    public int IdPrescription { get; set; }
    public int IdMedicament { get; set; }

    public int Dose { get; set; }
    [MaxLength(100)] public string Details { get; set; } = null!;

    [ForeignKey(nameof(IdPrescription))] public Prescription Prescription { get; set; } = null!;
    [ForeignKey(nameof(IdMedicament))] public Medicament Medicament { get; set; } = null!;
}