using CoreHealth.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceRESTGen6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionMedication> PrescriptionMedication { get; set; }
        public DbSet<ClinicHistory> ClinicHistory { get; set; }
    }
}
