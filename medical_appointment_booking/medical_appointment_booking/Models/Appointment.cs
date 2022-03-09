using System;
using System.Collections.Generic;

#nullable disable

namespace medical_appointment_booking.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DoctorId { get; set; }
        public bool? Bhty { get; set; }
        public bool? IsApproved { get; set; }
        public string Note { get; set; }
        public int? AccountId { get; set; }
        public string Result { get; set; }
        public virtual Account Account { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
