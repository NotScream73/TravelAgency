using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
