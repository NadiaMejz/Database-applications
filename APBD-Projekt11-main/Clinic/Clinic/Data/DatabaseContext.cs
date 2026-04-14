using System;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors   => Set<Doctor>();
    public DbSet<Medicament> Medicaments => Set<Medicament>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments => Set<PrescriptionMedicament>();

    protected DatabaseContext() { }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        b.Entity<Prescription>()
            .HasCheckConstraint("CK_Prescription_Dates", "[DueDate] >= [Date]");

        b.Entity<Doctor>().HasData(new Doctor { IdDoctor = 1, FirstName = "Gregory", LastName = "House", Email = "house@clinic.local" });
        b.Entity<Patient>().HasData(new Patient { IdPatient = 1, FirstName = "John", LastName = "Smith", Birthdate = new DateOnly(1980,1,1) });
    }
}