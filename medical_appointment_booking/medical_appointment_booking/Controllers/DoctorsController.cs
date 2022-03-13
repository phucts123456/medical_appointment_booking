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
    public class DoctorsController : ControllerBase
    {
        private readonly medicalappointmentbookingContext _context;

        public DoctorsController(medicalappointmentbookingContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.Include(d => d.Specialist).ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            Doctor curDoctor = await _context.Doctors.FindAsync(id);
            curDoctor.SpecialistId = doctor.SpecialistId;
            curDoctor.FullName = doctor.FullName;
            curDoctor.AcademicRank = doctor.AcademicRank;
            curDoctor.isMale = doctor.isMale;
            _context.Entry(curDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return curDoctor;
        }

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctorExists(doctor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("Admin")]
        public async Task<ActionResult<Doctor>> AddDoctorWithAccount(DoctorWithAccount doctorInput)
        {
            List<Account> lst = await _context.Accounts.ToListAsync();
            int AccountId = lst.Count();
            Account account = new Account();
            account.Id = AccountId + 1;
            account.UserName = doctorInput.UserName;
            account.Password = doctorInput.Password;
            account.Role = 2;
            
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                if (DoctorExists(account.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            List<Doctor> lstDoc = await _context.Doctors.ToListAsync();
            int DoctorId = lstDoc.Count();
            Doctor doctor = new Doctor();
            doctor.Id = DoctorId + 1;
            doctor.SpecialistId = doctorInput.SpecialistId;
            doctor.FullName = doctorInput.FullName;
            doctor.AcademicRank = doctorInput.AcademicRank;
            doctor.AccountId = account.Id;
            doctor.isMale = doctorInput.isMale;
            _context.Doctors.Add(doctor);
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                if (DoctorExists(doctor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            Account updateAccount = await _context.Accounts.FindAsync(account.Id);            
            updateAccount.DoctorID = doctor.Id;
            _context.Accounts.Update(updateAccount);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }
    }
}
