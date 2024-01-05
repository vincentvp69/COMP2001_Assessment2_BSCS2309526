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
    public class ActivityRecordsController : ControllerBase
    {
        private readonly Comp2001malVgohkahfungContext _context;

        public ActivityRecordsController(Comp2001malVgohkahfungContext context)
        {
            _context = context;
        }

        // GET: api/ActivityRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ActivityRecord>>> GetActivityRecords()
        {
            return await _context.ActivityRecords.ToListAsync();
        }

        // GET: api/ActivityRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ActivityRecord>> GetActivityRecord(int id)
        {
            var activityRecord = await _context.ActivityRecords.FindAsync(id);

            if (activityRecord == null)
            {
                return NotFound();
            }

            return activityRecord;
        }

        // PUT: api/ActivityRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityRecord(int id, Models.ActivityRecord activityRecord)
        {
            if (id != activityRecord.TrialId)
            {
                return BadRequest();
            }

            _context.Entry(activityRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityRecordExists(id))
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

        // POST: api/ActivityRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.ActivityRecord>> PostActivityRecord(Models.ActivityRecord activityRecord)
        {
            _context.ActivityRecords.Add(activityRecord);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActivityRecordExists(activityRecord.TrialId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActivityRecord", new { id = activityRecord.TrialId }, activityRecord);
        }

        // DELETE: api/ActivityRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityRecord(int id)
        {
            var activityRecord = await _context.ActivityRecords.FindAsync(id);
            if (activityRecord == null)
            {
                return NotFound();
            }

            _context.ActivityRecords.Remove(activityRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityRecordExists(int id)
        {
            return _context.ActivityRecords.Any(e => e.TrialId == id);
        }
    }
}
