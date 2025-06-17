using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class ServiceDTO: RegistryDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Servicio")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public decimal Cost { get; set; }
    }
}
