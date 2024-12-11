using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Helpers;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Accommodations;

namespace TravelAgency.Controllers;

[Authorize(Roles = "Admin, Manager")]
public class AccommodationsController : Controller
{
    private readonly AccommodationService _accommodationService;

    public AccommodationsController(AccommodationService accommodationService)
    {
        _accommodationService = accommodationService;
    }

    // GET: Accommodations
    public async Task<IActionResult> Index(string? name, int page = 0, int size = 5)
    {
        var filter = new AccommodationIndexFilterViewModel()
        {
            Name = name,
            Page = page,
            Size = size
        };

        (var list, var totalCount) = await _accommodationService.GetAllAsync(filter);

        filter.TotalCount = totalCount;

        var viewModel = new AccommodationIndexViewModel(
            filter: filter,
            list: list
        );

        return View(viewModel);
    }

    // GET: Accommodations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var accommodation = new AccommodationDetailsDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            accommodation = await _accommodationService.GetByIdAsync(id.Value);

            if (accommodation == null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new AccommodationDetailsViewModel(
            item: accommodation
        );

        return View(viewModel);
    }

    // GET: Accommodations/Create
    public IActionResult Create()
    {
        var item = _accommodationService.GetForCreate();

        var typeOptions = EnumHelper.GetEnumSelectList(item.Type);

        var viewModel = new AccommodationCreateViewModel(
            item: item,
            typeOptions: typeOptions
        );

        return View(viewModel);
    }

    // POST: Accommodations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync()
    {
        var accommodationDto = new AccommodationCreateDTO();
        try
        {
            if (await TryUpdateModelAsync(
                accommodationDto,
                "Item",
                i => i.Name,
                i => i.Description,
                i => i.Address,
                i => i.PricePerNight,
                i => i.Type
            ))
            {
                await _accommodationService.CreateAsync(accommodationDto);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var typeOptions = EnumHelper.GetEnumSelectList(accommodationDto.Type);

        var viewModel = new AccommodationCreateViewModel(
            item: accommodationDto,
            typeOptions: typeOptions
        );

        return View(viewModel);
    }

    // GET: Accommodations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var accommodation = new AccommodationEditDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            accommodation = await _accommodationService.GetForEditAsync(id.Value);
            if (accommodation == null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var typeOptions = EnumHelper.GetEnumSelectList(accommodation.Type);

        var viewModel = new AccommodationEditViewModel(
            item: accommodation,
            typeOptions: typeOptions
        );

        return View(viewModel);
    }

    // POST: Accommodations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id)
    {
        var accommodationDto = new AccommodationEditDTO();
        try
        {
            if (await TryUpdateModelAsync(
                accommodationDto,
                "Item",
                i => i.Id,
                i => i.Name,
                i => i.Description,
                i => i.Address,
                i => i.PricePerNight,
                i => i.Type
            ))
            {
                await _accommodationService.UpdateAsync(accommodationDto);
                return RedirectToAction("Details", new { id });
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var typeOptions = EnumHelper.GetEnumSelectList(accommodationDto.Type);

        var viewModel = new AccommodationEditViewModel(
            item: accommodationDto,
            typeOptions: typeOptions
        );

        return View(viewModel);
    }

    // GET: Accommodations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var accommodation = new AccommodationDeleteDTO();

        try
        {
            accommodation = await _accommodationService.GetForDeleteAsync(id.Value);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new AccommodationDeleteViewModel(
            item: accommodation
        );

        return View(viewModel);
    }

    // POST: Accommodations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _accommodationService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}
