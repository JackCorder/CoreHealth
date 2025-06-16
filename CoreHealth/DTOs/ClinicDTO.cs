using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class ClinicDTO
    {
        public int Id { get; set; }
        [Display(Name="Consultorio")]
        [Required(ErrorMessage ="Se requiere un nombre del consultorio")]
        public string Name { get; set; }
        [Display(Name = "Descripcion del consultorio")]
        public string? Description { get; set; }
        [Display(Name ="Doctor Asignado")]
        public int? DoctorId { get; set; }
        [Display(Name="Doctor Asignado")]
        public string? DoctorName {  get; set; }

    }
}
