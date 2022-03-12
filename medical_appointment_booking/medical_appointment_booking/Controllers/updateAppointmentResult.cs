using medical_appointment_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace medical_appointment_booking.Controllers
{
    [Route("api/updateResult")]
    [ApiController]
    public class updateAppointmentResult : Controller
    {
        private readonly medicalappointmentbookingContext _context;

        public updateAppointmentResult(medicalappointmentbookingContext context)
        {
            _context = context;
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }
            Appointment curAppointment = await _context.Appointments.FindAsync(id);
            curAppointment.Result = appointment.Result;
            _context.Entry(curAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
