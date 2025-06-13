namespace CoreHealth.Models
{
    public class PrescriptionMedication
    {
        public int PrescriptionId { get; set; }
        public int MedicationId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
    }
}
