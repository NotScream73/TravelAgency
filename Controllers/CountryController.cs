using Microsoft.AspNetCore.Mvc;

namespace TravelAgency.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
