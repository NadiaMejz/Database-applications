using AutoMapper;
using Clinic.DTOs;
using Clinic.Models;

namespace Clinic.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDetailsDto>();
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.Medicaments,
                cfg => cfg.MapFrom(s => s.MedicamentLinks));

        CreateMap<PrescriptionMedicament, MedicamentDto>()
            .ForMember(d => d.IdMedicament, cfg => cfg.MapFrom(s => s.IdMedicament))
            .ForMember(d => d.Name, cfg => cfg.MapFrom(s => s.Medicament.Name))
            .ForMember(d => d.Description, cfg => cfg.MapFrom(s => s.Medicament.Description))
            .ForMember(d => d.Dose, cfg => cfg.MapFrom(s => s.Dose))
            .ForMember(d => d.Details, cfg => cfg.MapFrom(s => s.Details));

        CreateMap<Doctor, DoctorDto>();

        CreateMap<PatientDto, Patient>();
    }
}