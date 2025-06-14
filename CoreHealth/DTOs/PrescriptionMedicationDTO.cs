using System.ComponentModel.DataAnnotations;

namespace CoreHealth.DTOs
{
    public class PrescriptionMedicationDTO
    {
        public int Id { get; set; }
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [Display(Name = "Medicamento")]
        public string MedicationName { get; set; }
        [Display(Name = "Dosis")]
        [Required]
        public string Dosage { get; set; }
        [Display(Name = "Frecuencia de Aplicación")]
        [Required]
        public string Frequency { get; set; }
        [Display(Name = "Duración")]
        [Required]
        public string Duration { get; set; }

    }
}
