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
    public class CoachRecordsController : ControllerBase
    {
        private readonly Comp2001malVgohkahfungContext _context;

        public CoachRecordsController(Comp2001malVgohkahfungContext context)
        {
            _context = context;
        }

        // GET: api/CoachRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachRecord>>> GetCoachRecords()
        {
            return await _context.CoachRecords.ToListAsync();
        }

        // GET: api/CoachRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoachRecord>> GetCoachRecord(int id)
        {
            var coachRecord = await _context.CoachRecords.FindAsync(id);

            if (coachRecord == null)
            {
                return NotFound();
            }

            return coachRecord;
        }

        // PUT: api/CoachRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoachRecord(int id, CoachRecord coachRecord)
        {
            if (id != coachRecord.ProfileId)
            {
                return BadRequest();
            }

            _context.Entry(coachRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachRecordExists(id))
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

        // POST: api/CoachRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoachRecord>> PostCoachRecord(CoachRecord coachRecord)
        {
            _context.CoachRecords.Add(coachRecord);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CoachRecordExists(coachRecord.ProfileId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCoachRecord", new { id = coachRecord.ProfileId }, coachRecord);
        }

        // DELETE: api/CoachRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoachRecord(int id)
        {
            var coachRecord = await _context.CoachRecords.FindAsync(id);
            if (coachRecord == null)
            {
                return NotFound();
            }

            _context.CoachRecords.Remove(coachRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachRecordExists(int id)
        {
            return _context.CoachRecords.Any(e => e.ProfileId == id);
        }
    }
}
