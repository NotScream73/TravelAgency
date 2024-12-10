using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Tours;

namespace TravelAgency.Controllers;

[Authorize]
public class ToursController : Controller
{
    private static readonly string FileDirectory = "wwwroot";

    private readonly TourService _tourService;
    private readonly CountryService _countryService;
    private readonly AccommodationService _accommodationService;
    private readonly ResortService _resortService;

    public ToursController(TourService tourService, CountryService countryService, AccommodationService accommodationService, ResortService resortService)
    {
        _tourService = tourService;
        _countryService = countryService;
        _accommodationService = accommodationService;
        _resortService = resortService;
    }

    // GET: Tours
    [AllowAnonymous]
    public async Task<IActionResult> Index(string? name, int page = 0, int size = 5)
    {
        var filter = new TourIndexFilterViewModel()
        {
            Name = name,
            Page = page,
            Size = size
        };

        (var list, var totalCount) = await _tourService.GetAllAsync(filter);

        filter.TotalCount = totalCount;

        var viewModel = new TourIndexViewModel(
            filter: filter,
            list: list
        );

        return View(viewModel);
    }

    // GET: Tours/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        var tour = new TourDetailsDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            tour = await _tourService.GetByIdAsync(id.Value);

            if (tour == null)
            {
                return NotFound();
            }
            if (!tour.PhotoPath.StartsWith('/'))
            {
                tour.PhotoPath = "/" + tour.PhotoPath;
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new TourDetailsViewModel(
            item: tour
        );

        return View(viewModel);
    }

    // GET: Tours/Create
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Create()
    {
        var item = _tourService.GetForCreate();

        var countryOptions =
            new SelectList(
                (await _countryService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var accommodationOptions =
            new SelectList(
                (await _accommodationService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var resortOptions =
            new SelectList(
                (await _resortService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var viewModel = new TourCreateViewModel(
            item: item,
            countryOptions: countryOptions,
            accommodationOptions: accommodationOptions,
            resortOptions: resortOptions
        );

        return View(viewModel);
    }

    // POST: Tours/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(IFormFile uploadedFile)
    {
        var tourDTO = new TourCreateDTO();
        string filePath = string.Empty;
        try
        {
            if (await TryUpdateModelAsync(
                tourDTO,
                "Item",
                i => i.Name,
                i => i.Description,
                i => i.Price,
                i => i.Count,
                i => i.StartDate,
                i => i.EndDate,
                i => i.CountryId,
                i => i.AccommodationId,
                i => i.ResortId
            ))
            {
                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    filePath = Path.Combine("/uploads", DateTime.Now.Ticks + uploadedFile.FileName);

                    using (var stream = new FileStream(FileDirectory + filePath, FileMode.OpenOrCreate))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                }
                await _tourService.CreateAsync(tourDTO, filePath);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex2)
            {
                ModelState.AddModelError("", ex2.Message);
            }
            ModelState.AddModelError("", ex.Message);
        }

        var countryOptions =
            new SelectList(
                (await _countryService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var accommodationOptions =
            new SelectList(
                (await _accommodationService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var resortOptions =
            new SelectList(
                (await _resortService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var viewModel = new TourCreateViewModel(
            item: tourDTO,
            countryOptions: countryOptions,
            accommodationOptions: accommodationOptions,
            resortOptions: resortOptions
        );

        return View(viewModel);
    }

    // GET: Tours/Edit/5
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Edit(int? id)
    {
        var tour = new TourEditDTO();

        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            tour = await _tourService.GetForEditAsync(id.Value);
            if (tour == null)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var countryOptions =
            new SelectList(
                (await _countryService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var accommodationOptions =
            new SelectList(
                (await _accommodationService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var resortOptions =
            new SelectList(
                (await _resortService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var viewModel = new TourEditViewModel(
            item: tour,
            countryOptions: countryOptions,
            accommodationOptions: accommodationOptions,
            resortOptions: resortOptions
        );

        return View(viewModel);
    }

    // POST: Countries/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> EditAsync(int id, IFormFile? uploadedFile)
    {
        var tourDto = new TourEditDTO();
        string filePath = string.Empty;
        try
        {
            if (await TryUpdateModelAsync(
                tourDto,
                "Item",
                i => i.Id,
                i => i.Name,
                i => i.Description,
                i => i.Price,
                i => i.Count,
                i => i.StartDate,
                i => i.EndDate,
                i => i.CountryId,
                i => i.AccommodationId,
                i => i.ResortId
            ))
            {
                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    filePath = Path.Combine("uploads", DateTime.Now.Ticks + uploadedFile.FileName);

                    using (var stream = new FileStream(FileDirectory + filePath, FileMode.OpenOrCreate))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                }
                await _tourService.UpdateAsync(tourDto, filePath);
                return RedirectToAction("Details", new { id });
            }
        }
        catch (Exception ex)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex2)
            {
                ModelState.AddModelError("", ex2.Message);
            }
            ModelState.AddModelError("", ex.Message);
        }

        var countryOptions =
            new SelectList(
                (await _countryService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var accommodationOptions =
            new SelectList(
                (await _accommodationService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var resortOptions =
            new SelectList(
                (await _resortService.GetAllForSelectAsync())
                    .Select(i => new
                    {
                        Value = i.Id,
                        Text = i.Name
                    }).ToList(),
                "Value", "Text",
                null);

        var viewModel = new TourEditViewModel(
            item: tourDto,
            countryOptions: countryOptions,
            accommodationOptions: accommodationOptions,
            resortOptions: resortOptions
        );

        return View(viewModel);
    }

    // GET: Tours/Delete/5
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tour = new TourDeleteDTO();

        try
        {
            tour = await _tourService.GetForDeleteAsync(id.Value);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new TourDeleteViewModel(
            item: tour
        );

        return View(viewModel);
    }

    // POST: Tours/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _tourService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}
