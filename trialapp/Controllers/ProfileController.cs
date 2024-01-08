using Microsoft.AspNetCore.Mvc;
using trialapp.Models;

namespace trialapp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Comp2001malVgohkahfungContext _context;

        public ProfileController(Comp2001malVgohkahfungContext context)
        {
            _context = context;
        }

        public async Task<ActionResult>Index()
        {
            // Verify session ID and get user ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                // User is not authenticated, redirect to login page
                return RedirectToAction("Login", "Account");
            }

            // Retrieve user information from the database
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId.Value);

            if (user == null)
            {
                // User not found in the database, handle this case as needed
                return RedirectToAction("Error");
            }

            // Populate the update profile form with user's current information
            var viewModel = new UserProfileViewModel
            {
                Username = user.Username,
                UserHeight = user.UserHeight,
                UserWeight = user.UserWeight,
                Email = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Invalid input, redisplay the form with validation errors
                return View("Index", viewModel);
            }

            // Retrieve user ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                // User is not authenticated, redirect to login page
                return RedirectToAction("Login", "Account");
            }

            // Update user information in the database
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId.Value);

            if (user == null)
            {
                // User not found in the database, handle this case as needed
                return RedirectToAction("Error");
            }

            // Update user information with values from the form
            user.Username = viewModel.Username;
            user.UserHeight = viewModel.UserHeight;
            user.UserWeight = viewModel.UserWeight;
            user.Email = viewModel.Email;

            _context.SaveChanges(); // Save changes to the database

            // Display a confirmation message
            ViewBag.SuccessMessage = "Profile updated successfully";

            return View("Index", viewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }

}
