using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class DoctorDTO : RegistryDTO
    {
        public int Id { get; set; }
        [Display(Name="Nombre del doctor")]
        [Required(ErrorMessage ="Se requiere un nombre de doctor")]
        public string Name { get; set; }
        [Display (Name="Especialización")]
        public string Area { get; set; } = "Doctor General";
        [Display(Name ="Licencia")]
        [Required(ErrorMessage ="Debe contar con licencia medica")]
        public string License { get; set; }
        [Display(Name="Telefono de contacto")]
        [Required(ErrorMessage ="Se requiere un numero de telefono")]
        public string Phone { get; set; }
        [Display(Name="Correo Electronico")]
        [Required(ErrorMessage ="Se requiere un correo electronico")]
        public string Email { get; set; }
    }
}
