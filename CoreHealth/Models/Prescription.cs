namespace CoreHealth.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
    }
}
