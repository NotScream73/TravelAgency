using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    public class AccommodationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
