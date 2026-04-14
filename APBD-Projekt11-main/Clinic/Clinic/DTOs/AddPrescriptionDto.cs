namespace Clinic.DTOs;

public class AddPrescriptionDto
{
    public int? IdPatient { get; set; }
    public PatientDto Patient { get; set; } = null!;
    public int IdDoctor { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<MedicamentOrderDto> Medicaments { get; set; } = new();
}

public class PatientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
}

public class MedicamentOrderDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; } = null!;
}