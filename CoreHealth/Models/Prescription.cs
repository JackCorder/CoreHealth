using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        //nav propierties
        public Appointment? Appointment { get; set; }
        public Doctor? Doctor { get; set; }
        public IEnumerable<PrescriptionMedication>? PrescriptionMedication { get; set; }
    }
}
