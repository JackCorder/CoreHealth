using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class RegistryDTO
    {
        [Display(Name = "Estatus")]
        public bool Active { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        [Display(Name = "Alta")]
        public DateTime HighSystem { get; set; } = DateTime.Now;
    }
}
