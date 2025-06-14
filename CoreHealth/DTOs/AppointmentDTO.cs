using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        [Display(Name ="Fecha de la cita")]
        [Required(ErrorMessage ="Se debe seleccionar una fecha")]
        public DateTime Date { get; set; }
        [Display(Name ="Paciente")]
        [Required(ErrorMessage ="Debe asignarse un paciente")]
        public int PatientId { get; set; }
        [Display(Name ="Consultorio")]
        [Required(ErrorMessage ="Debe seleccionarse un consultorio para la cita")]
        public int ClinicId { get; set; }
        [Display(Name ="Motivo de la cita")]
        [Required(ErrorMessage ="Debe espeficiar el motivo de la cita")]
        public string Reason { get; set; }
        [Display(Name ="Diagnostico Medico")]
        public string? Diagnostic { get; set; }
        [Display(Name ="Tratamiento recomendado")]
        public string? Treatment { get; set; }
        [Display(Name ="Servicio ofrecido")]
        public int? ServiceId { get; set; }
    }
}
