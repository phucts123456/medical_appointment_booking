using medical_appointment_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medical_appointment_booking.Controllers
{
    [Route("api/appsCustomGet")]
    [ApiController]
    public class updateAppointmentResult : Controller
    {
        private readonly medicalappointmentbookingContext _context;

        public updateAppointmentResult(medicalappointmentbookingContext context)
        {
            _context = context;
        }
        [Route("appListForDoc")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDocID(int docID,DateTime filterDate)
        {   
            if(filterDate == null)
            {
                return await _context.Appointments.Where(a => a.IsApproved == true && a.DoctorId == docID).ToListAsync();

            }
            return await _context.Appointments.Where(a => a.IsApproved == true && a.DoctorId == docID && filterDate.Equals(a.AppointmentDate)).ToListAsync();
        }
        [Route("appListbyAccID")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByAccID(int accID)
        {
           
                return await _context.Appointments.Where(a=> a.AccountId == accID).ToListAsync();

            
        }

        [HttpPut("/updateResult/{id}")]
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
