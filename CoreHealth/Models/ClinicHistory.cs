namespace CoreHealth.Models
{
    public class ClinicHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        //nav propierties
        public Patient? Patient { get; set; }
    }
}
