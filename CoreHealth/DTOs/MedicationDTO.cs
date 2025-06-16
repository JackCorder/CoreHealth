using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class MedicationDTO : RegistryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del medicamento es obligatorio")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Presentación")]
        [Required(ErrorMessage = "Se debe de indicar la presentaicón del medicamento")]
        public string Presentation { get; set; }

        [Required(ErrorMessage = "Se debe especificar la vía de administración")]
        [Display(Name = "Via de administración")]
        public string AdministrationWay { get; set; }


        public string UrlImage { get; set; }

    }
}
