using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medical_appointment_booking.Models;

namespace medical_appointment_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly medicalappointmentbookingContext _context;

        public SpecialistsController(medicalappointmentbookingContext context)
        {
            _context = context;
        }

        // GET: api/Specialists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialists()
        {
            return await _context.Specialists.ToListAsync();
        }

        // GET: api/Specialists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialist>> GetSpecialist(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);

            if (specialist == null)
            {
                return NotFound();
            }

            return specialist;
        }

        // PUT: api/Specialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialist(int id, Specialist specialist)
        {
            if (id != specialist.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialistExists(id))
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

        // POST: api/Specialists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialist>> PostSpecialist(Specialist specialist)
        {
            _context.Specialists.Add(specialist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpecialistExists(specialist.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpecialist", new { id = specialist.Id }, specialist);
        }

        // DELETE: api/Specialists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialist(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }

            _context.Specialists.Remove(specialist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}
