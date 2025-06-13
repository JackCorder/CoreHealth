using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Doctor
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
    }
}
