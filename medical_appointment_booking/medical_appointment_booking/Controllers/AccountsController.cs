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
    public class AccountsController : ControllerBase
    {
        private readonly medicalappointmentbookingContext _context;

        public AccountsController(medicalappointmentbookingContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
        [Route("loginWithGoogleID")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccountByGGID(String ID)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.googleAccountID == ID);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
    

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {   if(account.googleAccountID != null)
            {   
                if(await _context.Accounts.FirstOrDefaultAsync(a => a.googleAccountID == account.googleAccountID)!= null)
                {
                    return BadRequest("Account này đã tồn tại");
                }
            }
           int id = _context.Accounts.Count();
            account.Id = id+1;
            account.Role = 3;
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<Account>> SignIn(Account accountFrom)
        {
            var account = await _context.Accounts.Where(a => a.UserName.Equals(accountFrom.UserName) && a.Password.Equals(accountFrom.Password)).FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
    }
}
