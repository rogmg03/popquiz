using Microsoft.AspNetCore.Mvc;

namespace PopQuiz.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
