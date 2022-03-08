using System;
using System.Collections.Generic;

#nullable disable

namespace medical_appointment_booking.Models
{
    public partial class Specialist
    {
        public Specialist()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
