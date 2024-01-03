using Microsoft.AspNetCore.Mvc;

namespace trialapp.Views.User
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
