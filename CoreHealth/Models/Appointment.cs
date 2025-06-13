using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [Required]
        public string Reason { get; set; }
        public string? Diagnostic { get; set; }
        public string? Treatment { get; set; }
        public int? ServiceId { get; set; }

        //nav propierties
        public IEnumerable<Prescription>? Prescription { get; set; }
        public Patient? Patient { get; set; }
        public Clinic? Clinic { get; set; }
        public Service? Service { get; set; }
    }
}
