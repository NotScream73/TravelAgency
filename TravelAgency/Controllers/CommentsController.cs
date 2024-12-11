using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAgency.Data;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Comments;

namespace TravelAgency.Controllers;

[Authorize]
public class CommentsController : Controller
{
    private readonly CommentService _commentService;
    private readonly DataContext _dataContext;
    public CommentsController(CommentService commentService, DataContext dataContext)
    {
        _commentService = commentService;
        _dataContext = dataContext;
    }
    // GET: Accommodations
    public async Task<IActionResult> Index(int tourId, int page = 0, int size = 5)
    {
        var filter = new CommentIndexFilterViewModel()
        {
            TourId = tourId,
            Page = page,
            Size = size
        };

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return NotFound();
        }

        (var item, var list) = await _commentService.GetForIndexAsync(filter, userId);

        item.TourName =
            _dataContext.Tours
                .Where(i => i.Id == tourId)
                .Select(i => i.Name)
                .FirstOrDefault();

        item.TourId = tourId;

        var viewModel = new CommentIndexViewModel(
            filter: filter,
            list: list,
            item: item
        );

        return View(viewModel);
    }
    // GET: Accommodations
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(int page = 0, int size = 5)
    {
        var commentDTO = new CommentEditDTO();
        var filter = new CommentIndexFilterViewModel()
        {
            Page = page,
            Size = size
        };

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return NotFound();
        }

        try
        {
            if (await TryUpdateModelAsync(
                commentDTO,
                "Item",
                i => i.Score,
                i => i.Message,
                i => i.TourId
            ))
            {
                filter.TourId = commentDTO.TourId;
                await _commentService.CreateAsync(commentDTO, userId);
                return RedirectToAction(nameof(Index), filter);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        filter.TourId = commentDTO.TourId;
        (_, var list1) = await _commentService.GetForIndexAsync(filter, userId);

        (var item, var list) = await _commentService.GetForIndexAsync(filter, userId);

        item.TourName =
            _dataContext.Tours
                .Where(i => i.Id == commentDTO.TourId)
                .Select(i => i.Name)
                .FirstOrDefault();

        item.TourId = commentDTO.TourId;

        var viewModel = new CommentIndexViewModel(
            filter: filter,
            list: list1,
            item: commentDTO
        );

        return View(viewModel);
    }
}
