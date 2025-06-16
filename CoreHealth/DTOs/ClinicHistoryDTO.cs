using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class ClinicHistoryDTO : RegistryDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [Display(Name = "Paciente")]
        public string PatientName { get; set; }
        [Display(Name = "Fecha Intervención")]
        public DateTime Date { get; set; }
        [Display(Name = "Descripción")]
        public string? Description { get; set; }
    }
}
