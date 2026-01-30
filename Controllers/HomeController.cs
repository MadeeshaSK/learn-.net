//Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
