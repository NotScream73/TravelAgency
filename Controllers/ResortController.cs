using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    public class ResortController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
