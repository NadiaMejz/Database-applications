using Clinic.DTOs;

namespace Clinic.Services;

public interface IDbService
{
    Task<int> AddPrescription(AddPrescriptionDto dto);
    Task<PatientDetailsDto?> GetPatient(int id);
}