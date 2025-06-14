using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del paciente es obligatorio")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Género")]
        public string? Gender { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Dirección")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "El numero de teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Número de seguro Social")]
        public string? NSS { get; set; }
    }
}
