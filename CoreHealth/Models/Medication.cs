using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Medication
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Presentation { get; set; }
        [Required]
        public string AdministrationWay { get; set; }

        //Nav Propierties

    }
}
