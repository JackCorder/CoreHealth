using CoreHealth.Models;
using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }

        [Display(Name = "Nombre del Doctor")]
        public string? doctorName { get; set; }
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
    }
}
