using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trialapp.Models;

namespace trialapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuditLogsController : ControllerBase
    {
        private readonly Comp2001malVgohkahfungContext _context;

        public UserAuditLogsController(Comp2001malVgohkahfungContext context)
        {
            _context = context;
        }

        // GET: api/UserAuditLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAuditLog>>> GetUserAuditLogs()
        {
            return await _context.UserAuditLogs.ToListAsync();
        }

        // GET: api/UserAuditLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuditLog>> GetUserAuditLog(int id)
        {
            var userAuditLog = await _context.UserAuditLogs.FindAsync(id);

            if (userAuditLog == null)
            {
                return NotFound();
            }

            return userAuditLog;
        }

        // PUT: api/UserAuditLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAuditLog(int id, UserAuditLog userAuditLog)
        {
            if (id != userAuditLog.AuditLogId)
            {
                return BadRequest();
            }

            _context.Entry(userAuditLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAuditLogExists(id))
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

        // POST: api/UserAuditLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAuditLog>> PostUserAuditLog(UserAuditLog userAuditLog)
        {
            _context.UserAuditLogs.Add(userAuditLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAuditLog", new { id = userAuditLog.AuditLogId }, userAuditLog);
        }

        // DELETE: api/UserAuditLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAuditLog(int id)
        {
            var userAuditLog = await _context.UserAuditLogs.FindAsync(id);
            if (userAuditLog == null)
            {
                return NotFound();
            }

            _context.UserAuditLogs.Remove(userAuditLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAuditLogExists(int id)
        {
            return _context.UserAuditLogs.Any(e => e.AuditLogId == id);
        }
    }
}
