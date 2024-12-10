using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Purchases;

namespace TravelAgency.Services;

public class PurchaseService
{
    private readonly DataContext _dataContext;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public PurchaseService(DataContext dataContext, UserManager<User> userManager, IMapper mapper)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<int> IncreaseTourToUser(int tourId, string userId)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(i => i.Id == userId);

        var tour =
            await _dataContext.Tours
                .FirstOrDefaultAsync(i => i.Id == tourId)
            ?? throw new Exception("Тур не найден");

        if (tour.Count <= 0)
        {
            throw new Exception("Билеты на тур закончились");
        }

        var userPurchase =
            await _dataContext.Purchases
                .FirstOrDefaultAsync(i => i.UserId == userId && i.Status == PurchaseStatus.Created);

        using var tr = _dataContext.Database.BeginTransaction();

        if (userPurchase == null)
        {
            userPurchase = new Purchase
            {
                UserId = userId,
                Created = DateTime.UtcNow,
                Status = PurchaseStatus.Created,
                TotalPrice = 0
            };

            await _dataContext.Purchases.AddAsync(userPurchase);

            await _dataContext.SaveChangesAsync();
        }

        var tourPurchase =
            await _dataContext.TourPurchases
                .FirstOrDefaultAsync(i => i.PurchaseId == userPurchase.Id && i.TourId == tourId);

        if (tourPurchase == null)
        {
            tourPurchase = new TourPurchase
            {
                PurchaseId = userPurchase.Id,
                TourId = tourId,
                Price = tour.Price,
                Count = 1
            };
            userPurchase.TotalPrice += tour.Price;

            await _dataContext.TourPurchases.AddAsync(tourPurchase);
        }
        else
        {
            if (tourPurchase.Count < tour.Count)
            {
                userPurchase.TotalPrice += tour.Price;
                tourPurchase.Count += 1;
            }
        }

        await _dataContext.SaveChangesAsync();
        await tr.CommitAsync();

        return tourPurchase.Count;
    }

    public async Task<int> DecreaseTourToUser(int tourId, string userId)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(i => i.Id == userId);

        var tour =
            await _dataContext.Tours
                .FirstOrDefaultAsync(i => i.Id == tourId)
            ?? throw new Exception("Тур не найден");

        var userPurchase =
            await _dataContext.Purchases
                .FirstOrDefaultAsync(i => i.UserId == userId && i.Status == PurchaseStatus.Created);

        if (userPurchase == null)
        {
            throw new Exception("Заказ не найден");
        }

        var tourPurchase =
            await _dataContext.TourPurchases
                .FirstOrDefaultAsync(i => i.PurchaseId == userPurchase.Id && i.TourId == tourId);

        if (tourPurchase == null)
        {
            throw new Exception("Необходимо иметь хотя бы 1 билет");
        }

        int count;
        if (tourPurchase.Count == 1)
        {
            _dataContext.TourPurchases.Remove(tourPurchase);
            count = 0;
        }
        else
        {
            tourPurchase.Count -= 1;
            count = tourPurchase.Count;
        }

        userPurchase.TotalPrice -= tourPurchase.Price;

        await _dataContext.SaveChangesAsync();

        return count;
    }

    public async Task<(List<PurchaseListDTO> list, int totalCount)> GetListByUserIdAsync(ClaimsPrincipal user, PurchaseIndexFilterViewModel filter)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var userEntity = await _dataContext.Users.FirstOrDefaultAsync(i => i.Id == userId);

        var isUser = await _userManager.IsInRoleAsync(userEntity, "User");

        IQueryable<PurchaseListDTO> query;

        if (isUser)
        {
            query =
                from p in _dataContext.Purchases
                join t in _dataContext.TourPurchases on p.Id equals t.PurchaseId
                where p.UserId == userId && p.Status != PurchaseStatus.Created
                select new PurchaseListDTO
                {
                    Id = p.Id,
                    TotalPrice = p.TotalPrice,
                    Created = p.Created,
                    Status = p.Status,
                    ProductCount = _dataContext.TourPurchases.Where(i => i.PurchaseId == p.Id).Sum(i => i.Count)
                };
        }
        else
        {
            query =
                from p in _dataContext.Purchases
                join t in _dataContext.TourPurchases on p.Id equals t.PurchaseId
                select new PurchaseListDTO
                {
                    Id = p.Id,
                    TotalPrice = p.TotalPrice,
                    Created = p.Created,
                    Status = p.Status,
                    ProductCount = _dataContext.TourPurchases.Where(i => i.PurchaseId == p.Id).Sum(i => i.Count)
                };
        }

        if (!isUser && !string.IsNullOrEmpty(filter.UserId))
        {
            query =
                from q in query
                join p in _dataContext.Purchases on q.Id equals p.Id
                where p.UserId == userId
                select q;
        }

        var totalCount = query.Count();

        query = query
            .Skip(filter.Page * filter.Size)
            .Take(filter.Size);

        return (await query.ToListAsync(), totalCount);
    }

    public async Task<List<PurchaseIndexDTO>> GetByUserIdAsync(string userId)
    {
        return await
            (
                from p in _dataContext.Purchases
                join tp in _dataContext.TourPurchases on p.Id equals tp.PurchaseId
                join t in _dataContext.Tours on tp.TourId equals t.Id
                where p.UserId == userId && p.Status == PurchaseStatus.Created
                select new PurchaseIndexDTO
                {
                    Id = t.Id,
                    Price = t.Price,
                    TourName = t.Name,
                    ProductCount = tp.Count
                }
            ).ToListAsync();
    }

    public async Task<int> CreateByUserId(string userId)
    {
        var userPurchase = await
            (
                from p in _dataContext.Purchases
                where p.UserId == userId && p.Status == PurchaseStatus.Created
                select new
                {
                    Purchase = p,
                    TourPurchases = _dataContext.TourPurchases.Where(i => i.PurchaseId == p.Id).ToList(),
                    Tours =
                        (
                            from tp in _dataContext.TourPurchases
                            join t in _dataContext.Tours on tp.TourId equals t.Id
                            where tp.PurchaseId == p.Id
                            select t
                        ).ToList()
                }
            ).FirstOrDefaultAsync();

        if (userPurchase == null || userPurchase.TourPurchases.Count == 0)
        {
            throw new Exception("Корзина пуста");
        }
        decimal totalSum = 0;

        foreach (var item in userPurchase.TourPurchases)
        {
            var tour = userPurchase.Tours.First(i => i.Id == item.TourId);

            if (tour.Count < item.Count)
            {
                throw new Exception("Количества билетов не хватает");
            }

            tour.Count -= item.Count;
            item.Price = tour.Price;

            totalSum += item.Count * item.Price;

        }

        userPurchase.Purchase.TotalPrice = totalSum;
        userPurchase.Purchase.Status = PurchaseStatus.Confirmed;

        await _dataContext.SaveChangesAsync();

        return userPurchase.Purchase.Id;
    }

    public async Task<(PurchaseDetailsItemDTO item, List<PurchaseDetailsDTO> list)> GetByIdAsync(int purchaseId, string userId)
    {
        var user = await _dataContext.Users.FirstAsync(i => i.Id == userId);
        var isUser = await _userManager.IsInRoleAsync(user, "User");

        var tourPurchaseList = await
            (
                from p in _dataContext.Purchases
                join tp in _dataContext.TourPurchases on p.Id equals tp.PurchaseId
                join t in _dataContext.Tours on tp.TourId equals t.Id
                where p.Id == purchaseId && (!isUser || userId == p.UserId)
                select new PurchaseDetailsDTO
                {
                    Id = t.Id,
                    Price = t.Price,
                    TourName = t.Name,
                    ProductCount = tp.Count,
                    PhotoPath = t.PhotoPath
                }
            ).ToListAsync();

        var item = await
            _dataContext.Purchases
                .Where(i => i.Id == purchaseId)
                .ProjectTo<PurchaseDetailsItemDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        return (item, tourPurchaseList);
    }
}
