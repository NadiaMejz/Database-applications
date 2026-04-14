using AutoMapper;
using Clinic.Data;
using Clinic.DTOs;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _db;
    private readonly IMapper _mapper;

    public DbService(DatabaseContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> AddPrescription(AddPrescriptionDto dto)
    {
        if (dto.Medicaments.Count is 0 or > 10)
            throw new ArgumentException("Prescription must contain 1–10 medicaments.");

        var doctor = await _db.Doctors.FindAsync(dto.IdDoctor)
                     ?? throw new ArgumentException("Doctor not found.");

        var mIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
        var found = await _db.Medicaments.Where(m => mIds.Contains(m.IdMedicament))
                                         .Select(m => m.IdMedicament)
                                         .ToListAsync();
        if (found.Count != mIds.Count)
            throw new ArgumentException("One or more medicaments don’t exist.");

        Patient patient;
        if (dto.IdPatient.HasValue)
        {
            patient = await _db.Patients.FindAsync(dto.IdPatient.Value)
                      ?? throw new ArgumentException("Patient not found.");
        }
        else
        {
            patient = _mapper.Map<Patient>(dto.Patient);
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            Patient = patient,
            Doctor = doctor
        };

        prescription.MedicamentLinks = dto.Medicaments.Select(m => new PrescriptionMedicament
        {
            Medicament = _db.Medicaments.Local.First(e => e.IdMedicament == m.IdMedicament)
                        ?? new Medicament { IdMedicament = m.IdMedicament },
            Dose = m.Dose,
            Details = m.Details
        }).ToList();

        _db.Prescriptions.Add(prescription);
        await _db.SaveChangesAsync();

        return prescription.IdPrescription;
    }

    public async Task<PatientDetailsDto?> GetPatient(int id)
    {
        var patient = await _db.Patients
            .Include(p => p.Prescriptions.OrderBy(pr => pr.DueDate))
                .ThenInclude(pr => pr.MedicamentLinks)
                    .ThenInclude(link => link.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        return patient is null ? null : _mapper.Map<PatientDetailsDto>(patient);
    }
}