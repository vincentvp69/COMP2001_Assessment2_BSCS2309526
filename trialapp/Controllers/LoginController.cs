
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trialapp.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;


namespace trialapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Comp2001malVgohkahfungContext _context;
        private readonly ILogger _logger;
        public LoginController(IHttpClientFactory httpClientFactory, Comp2001malVgohkahfungContext context,ILogger<LoginController>logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await VerifyUserAsync(email, password);
            _logger.LogInformation($"result: +{ result}");
            if (result)
            {
                
                    return RedirectToAction("Privacy", "Home");
                
            }

            // Logic for failed verification
            ModelState.AddModelError("", "Invalid login attempt.");

            return View("~/Views/Home/Index.cshtml");

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
                            return true;
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
