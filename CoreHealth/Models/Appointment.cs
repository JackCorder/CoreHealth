namespace CoreHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
        public string? Reason { get; set; }
        public string? Diagnostic { get; set; }
        public string? Treatment { get; set; }
        public int ServiceId { get; set; }
    }
}
