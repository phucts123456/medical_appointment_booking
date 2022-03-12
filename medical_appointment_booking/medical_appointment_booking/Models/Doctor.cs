using System;
using System.Collections.Generic;

#nullable disable

namespace medical_appointment_booking.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int? SpecialistId { get; set; }
        public string FullName { get; set; }
        public string AcademicRank { get; set; }
        public int? AccountId { get; set; }
        public bool isMale { get; set; }
        public virtual Account Account { get; set; }
        public virtual Specialist Specialist { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
