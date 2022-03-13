using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medical_appointment_booking.Controllers
{
    public class DoctorWithAccount
    {
        public int? SpecialistId { get; set; }
        public string FullName { get; set; }
        public string AcademicRank { get; set; }
        public bool isMale { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
