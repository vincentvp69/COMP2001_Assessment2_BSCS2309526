using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trialapp.Models;

namespace trialapp.Controllers
{
    public class ActivityRecordController : Controller
    {
        private readonly Comp2001malVgohkahfungContext _context; // Replace with your actual context

        public ActivityRecordController(Comp2001malVgohkahfungContext context)
        {
            _context = context;
        }

        // GET: /ActivityRecord/
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivityRecords.ToListAsync()); // Replace 'ActivityRecords' with your actual DbSet name
        }
    }
}
