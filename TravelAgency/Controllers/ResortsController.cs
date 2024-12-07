using Microsoft.AspNetCore.Mvc;
using TravelAgency.Helpers;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Resorts;

namespace TravelAgency.Controllers
{
    public class ResortsController : Controller
    {
        private readonly ResortService _resortService;

        public ResortsController(ResortService resortService)
        {
            _resortService = resortService;
        }

        // GET: Resorts
        public async Task<IActionResult> Index(string? name, int page = 0, int size = 5)
        {
            var filter = new ResortIndexFilterViewModel()
            {
                Name = name,
                Page = page,
                Size = size
            };

            (var list, var totalCount) = await _resortService.GetAllAsync(filter);

            filter.TotalCount = totalCount;

            var viewModel = new ResortIndexViewModel(
                filter: filter,
                resortList: list
            );

            return View(viewModel);
        }

        // GET: Resorts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var resort = new ResortDetailsDTO();

            try
            {
                if (!id.HasValue)
                {
                    return NotFound();
                }

                resort = await _resortService.GetByIdAsync(id.Value);

                if (resort == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var viewModel = new ResortDetailsViewModel(
                item: resort
            );

            return View(viewModel);
        }

        // GET: Resorts/Create
        public IActionResult Create()
        {
            var item = _resortService.GetForCreate();

            var typeOptions = EnumHelper.GetEnumSelectList(item.Type);

            var viewModel = new ResortCreateViewModel(
                item: item,
                typeOptions: typeOptions
            );

            return View(viewModel);
        }

        // POST: Resorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync()
        {
            var resortDto = new ResortCreateDTO();
            try
            {
                if (await TryUpdateModelAsync(
                    resortDto,
                    "Item",
                    i => i.Name,
                    i => i.Description,
                    i => i.Type
                ))
                {
                    await _resortService.CreateAsync(resortDto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var typeOptions = EnumHelper.GetEnumSelectList(resortDto.Type);

            var viewModel = new ResortCreateViewModel(
                item: resortDto,
                typeOptions: typeOptions
            );

            return View(viewModel);
        }

        // GET: Resorts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var resort = new ResortEditDTO();

            try
            {
                if (!id.HasValue)
                {
                    return NotFound();
                }

                resort = await _resortService.GetForEditAsync(id.Value);
                if (resort == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var typeOptions = EnumHelper.GetEnumSelectList(resort.Type);

            var viewModel = new ResortEditViewModel(
                item: resort,
                typeOptions: typeOptions
            );

            return View(viewModel);
        }

        // POST: Resorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var resortDto = new ResortEditDTO();
            try
            {
                if (await TryUpdateModelAsync(
                    resortDto,
                    "Item",
                    i => i.Id,
                    i => i.Name,
                    i => i.Description,
                    i => i.Type
                ))
                {
                    await _resortService.UpdateAsync(resortDto);
                    return RedirectToAction("Details", new { id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var typeOptions = EnumHelper.GetEnumSelectList(resortDto.Type);

            var viewModel = new ResortEditViewModel(
                item: resortDto,
                typeOptions: typeOptions
            );

            return View(resortDto);
        }

        // GET: Resorts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resort = new ResortDeleteDTO();

            try
            {
                resort = await _resortService.GetForDeleteAsync(id.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var viewModel = new ResortDeleteViewModel(
                item: resort
            );

            return View(viewModel);
        }

        // POST: Resorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _resortService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
