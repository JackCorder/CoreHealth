using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class PrescriptionMedication
    {
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [Required]
        public string Dosage { get; set; }
        [Required]
        public string Frequency { get; set; }
        [Required]
        public string Duration { get; set; }

        //nav propierties
        public Prescription? Prescription { get; set; }
        public Medication? Medication { get; set; }
    }
}
