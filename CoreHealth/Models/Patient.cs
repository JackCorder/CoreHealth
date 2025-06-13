using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string? Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string? NSS { get; set; }

        //Nav Propierties
    }
}
