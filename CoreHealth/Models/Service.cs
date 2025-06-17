using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Service : Registry
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Cost { get; set; }

        //nav propierties 
        public IEnumerable<Appointment>? Appointment { get; set; }
    }
}
