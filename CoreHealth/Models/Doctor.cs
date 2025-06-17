using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Doctor : Registry
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        public string Area { get; set; } = "Doctor General";
        [Required]
        public string License { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        //nav propierties
        public IEnumerable<Appointment>? Appointment { get; set; }
        public IEnumerable<Clinic>? Clinic { get; set; }
        public IEnumerable<Prescription>? Prescription { get; set; }

    }
}
