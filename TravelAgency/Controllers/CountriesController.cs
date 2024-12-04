using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountryService _service;

        public CountriesController(CountryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllCountriesAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var country = await _service.GetCountryByIdAsync(id.Value);
            if (country == null) return NotFound();

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                if (await _service.CreateCountryAsync(country))
                    return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var country = await _service.GetCountryByIdAsync(id.Value);
            if (country == null) return NotFound();

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (await _service.UpdateCountryAsync(country))
                    return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var country = await _service.GetCountryByIdAsync(id.Value);
            if (country == null) return NotFound();

            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _service.DeleteCountryAsync(id))
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }

}
