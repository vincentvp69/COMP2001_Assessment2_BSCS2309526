
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trialapp.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http; // Add this namespace for HttpContextAccessor
using System.Text.Json;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;


namespace trialapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Comp2001malVgohkahfungContext _context;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Inject IHttpContextAccessor

        public LoginController(IHttpClientFactory httpClientFactory, Comp2001malVgohkahfungContext context,ILogger<LoginController>logger, IHttpContextAccessor httpContextAccessor) // Inject IHttpContextAccessor
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await VerifyUserAsync(email, password);
            _logger.LogInformation($"result: +{ result}");
            if (result)
            {
                
                    return RedirectToAction("UpdateProfile", "Home");
                
            }

            // Logic for failed verification
            ModelState.AddModelError("", "Invalid login attempt.");

            return View("~/Views/Home/Index.cshtml");

        }
        private async Task<int?> GetUserIdByEmailAsync(string email)
        {
            // Assuming you have a User model and a DbSet for users in your DbContext
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user.UserId; // Replace 'Id' with the actual property name for the user ID.
            }
            return null;
        }
        private async Task<bool> VerifyUserAsync(string email, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var url = "https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users";

            var userCredentials = new
            {
                email,
                password
            };
            var json = JsonConvert.SerializeObject(userCredentials, Formatting.Indented);
            _logger.LogInformation($"User Credentials: {json}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _logger.LogInformation($"Request Content: {await content.ReadAsStringAsync()}");


            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var verificationResult = System.Text.Json.JsonSerializer.Deserialize<List<string>>(responseString);
                        _logger.LogInformation($"Verification Result: {string.Join(", ", verificationResult)}");
                        _logger.LogInformation($"User Credentials: {email + " " + password}");

                        // Check if the API response contains ["Verified", "True"]
                        if (verificationResult != null &&
                            verificationResult.Count == 2 &&
                            verificationResult[0] == "Verified" &&
                            verificationResult[1] == "True")
                        {
                            // Get the user ID from the hosting database
                            var userId = await GetUserIdByEmailAsync(email);
                            if (userId.HasValue) // Check if userId has a value
                            {
                                _logger.LogInformation($"User ID: {userId.Value}");
                                HttpContext.Session.SetInt32("UserId", userId.Value); // Use userId.Value to extract the non-nullable int
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }
    }
}
