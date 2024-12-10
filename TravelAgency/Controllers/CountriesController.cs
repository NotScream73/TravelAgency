using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Countries;

namespace TravelAgency.Controllers;

[Authorize(Roles = "Admin, Manager")]
public class CountriesController : Controller
{
    private readonly CountryService _countryService;

    public CountriesController(CountryService countryService)
    {
        _countryService = countryService;
    }

    // GET: Countries
    public async Task<IActionResult> Index(string? name, int page = 0, int size = 5)
    {
        var filter = new CountryIndexFilterViewModel()
        {
            Name = name,
            Page = page,
            Size = size
        };

        (var list, var totalCount) = await _countryService.GetAllAsync(filter);

        filter.TotalCount = totalCount;

        var viewModel = new CountryIndexViewModel(
            filter: filter,
            list: list
        );

        return View(viewModel);
    }

    // GET: Countries/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var country = new CountryDetailsDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            country = await _countryService.GetByIdAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new CountryDetailsViewModel(
            item: country
        );

        return View(viewModel);
    }

    // GET: Countries/Create
    public IActionResult Create()
    {
        var item = _countryService.GetForCreate();

        var viewModel = new CountryCreateViewModel(
            item: item
        );

        return View(viewModel);
    }

    // POST: Countries/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync()
    {
        var countryDto = new CountryCreateDTO();
        try
        {
            if (await TryUpdateModelAsync(
                countryDto,
                "Item",
                i => i.Name
            ))
            {
                await _countryService.CreateAsync(countryDto);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new CountryCreateViewModel(
            item: countryDto
        );

        return View(viewModel);
    }

    // GET: Countries/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var country = new CountryEditDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            country = await _countryService.GetForEditAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new CountryEditViewModel(
            item: country
        );

        return View(viewModel);
    }

    // POST: Countries/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id)
    {
        var countryDto = new CountryEditDTO();
        try
        {
            if (await TryUpdateModelAsync(
                countryDto,
                "Item",
                i => i.Id,
                i => i.Name
            ))
            {
                await _countryService.UpdateAsync(countryDto);
                return RedirectToAction("Details", new { id });
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new CountryEditViewModel(
            item: countryDto
        );

        return View(viewModel);
    }

    // GET: Countries/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var country = new CountryDeleteDTO();

        try
        {
            country = await _countryService.GetForDeleteAsync(id.Value);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new CountryDeleteViewModel(
            item: country
        );

        return View(viewModel);
    }

    // POST: Countries/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _countryService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}
