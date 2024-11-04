using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
