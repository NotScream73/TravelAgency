using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Purchases;

namespace TravelAgency.Controllers;

[Authorize]
public class PurchasesController : Controller
{
    private readonly PurchaseService _purchaseService;

    public PurchasesController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> Increase(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var count = await _purchaseService.IncreaseTourToUser(id, userId);
            return Json(new { success = true, count, message = "Покупка успешно совершена" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Decrease(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var count = await _purchaseService.DecreaseTourToUser(id, userId);

            return Json(new { success = true, count });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // GET: Purchases/List
    public async Task<IActionResult> List(string? userId, int page = 0, int size = 5)
    {
        var filter = new PurchaseIndexFilterViewModel()
        {
            UserId = userId,
            Page = page,
            Size = size
        };

        (var list, var totalCount) = await _purchaseService.GetListByUserIdAsync(User, filter);

        filter.TotalCount = totalCount;

        var viewModel = new PurchaseListViewModel(
            list: list,
            filter: filter
        );

        return View(viewModel);
    }

    // GET: Purchases/Index
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new Exception("Пользователь не найден");
        }

        var list = await _purchaseService.GetByUserIdAsync(userId);

        var viewModel = new PurchaseIndexViewModel(
            list: list
        );

        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var purchaseId = await _purchaseService.CreateByUserId(userId);
            return RedirectToAction("Details", new { id = purchaseId });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    // GET: Purchases/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        (PurchaseDetailsItemDTO item, List<PurchaseDetailsDTO> list) = (null, null);
        try
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("Пользователь не найден");
            }

            (item, list) = await _purchaseService.GetByIdAsync(id.Value, userId);

            if (item == null || list == null)
            {
                return NotFound();
            }

            list.ForEach(tourPurchase =>
            {
                if (!tourPurchase.PhotoPath.StartsWith('/'))
                {
                    tourPurchase.PhotoPath = "/" + tourPurchase.PhotoPath;
                }
            });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var viewModel = new PurchaseDetailsViewModel(
            item: item,
            tourPurchaseList: list
        );

        return View(viewModel);
    }
}
