using System;
using System.Collections.Generic;

#nullable disable

namespace medical_appointment_booking.Models
{
    public partial class Account
    {
        public Account()
        {
            Appointments = new HashSet<Appointment>();
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
        public int? DotorID { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
