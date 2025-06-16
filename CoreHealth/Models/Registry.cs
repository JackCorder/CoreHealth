namespace CoreHealth.Models
{
    public class Registry
    {
        public bool Active { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime HighSystem { get; set; } = DateTime.Now;
    }
}
