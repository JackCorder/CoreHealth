﻿using System.ComponentModel.DataAnnotations;

namespace CoreHealth.Models
{
    public class Clinic : Registry
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? DoctorId { get; set; }

        //nav propierties

        public IEnumerable<Appointment>? Appointment { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
