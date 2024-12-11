using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Tours;

namespace TravelAgency.Services;

public class TourService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public TourService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<(List<TourListDTO> list, int totalCount)> GetAllAsync(TourIndexFilterViewModel filter)
    {
        var query =
            (
                from t in _dataContext.Tours
                join co in _dataContext.Countries on t.CountryId equals co.Id
                join r in _dataContext.Resorts on t.ResortId equals r.Id
                select new TourListDTO
                {
                    Count = t.Count,
                    CountryName = co.Name,
                    EndDate = t.EndDate,
                    Id = t.Id,
                    Name = t.Name,
                    PhotoPath = t.PhotoPath,
                    Price = t.Price,
                    ResortName = r.Name,
                    StartDate = t.StartDate,
                    Score = _dataContext.Comments.Where(i => i.TourId == t.Id).Count() != 0 ? (double)_dataContext.Comments.Where(i => i.TourId == t.Id).Sum(i => i.Score) / _dataContext.Comments.Where(i => i.TourId == t.Id).Count() : 0
                }
            );

        _dataContext.Tours
            .ProjectTo<TourListDTO>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(q => EF.Functions.Like(q.Name.ToLower(), '%' + filter.Name.ToLower() + '%'));
        }

        var totalCount = query.Count();

        query = query
            .Skip(filter.Page * filter.Size)
            .Take(filter.Size);

        return (await query.ToListAsync(), totalCount);
    }

    public async Task<TourDetailsDTO> GetByIdAsync(int id)
    {
        var tour = await _dataContext.Tours.ProjectTo<TourDetailsDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(i => i.Id == id);

        if (tour == null) throw new Exception("Тур не найден");

        return tour;
    }

    public TourCreateDTO GetForCreate()
    {
        return new TourCreateDTO
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(14),
        };
    }

    public async Task<TourEditDTO> GetForEditAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Тур не найден");
        }

        var tour = await _dataContext.Tours.ProjectTo<TourEditDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (tour == null) throw new Exception("Тур не найден");

        return tour;
    }

    public async Task<TourDeleteDTO> GetForDeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Тур не найден");
        }

        var tour = await _dataContext.Tours.ProjectTo<TourDeleteDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (tour == null) throw new Exception("Тур не найден");

        return tour;
    }

    public async Task CreateAsync(TourCreateDTO tourCreateDTO, string filePath)
    {
        if (tourCreateDTO.EndDate < tourCreateDTO.StartDate)
        {
            throw new Exception("Дата окончания не может быть раньше даты начала");
        }

        var accommodationPrice =
            _dataContext.Accommodations
                .Where(i => i.Id == tourCreateDTO.AccommodationId)
                .Select(i => i.PricePerNight)
                .FirstOrDefault();

        if (accommodationPrice * (tourCreateDTO.EndDate - tourCreateDTO.StartDate).Days > tourCreateDTO.Price)
        {
            throw new Exception("Стоимость тура не может быть меньше общих затрат на проживание");
        }

        var tour = _mapper.Map<Tour>(tourCreateDTO);

        tour.PhotoPath = filePath;

        await _dataContext.Tours.AddAsync(tour);
        await _dataContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(TourEditDTO tourDto, string filePath)
    {
        var tour = await _dataContext.Tours.FirstOrDefaultAsync(r => r.Id == tourDto.Id);

        if (tour == null) throw new Exception("Тур не найден");
        if (tourDto.EndDate < tourDto.StartDate)
        {
            throw new Exception("Дата окончания не может быть раньше даты начала");
        }

        var accommodationPrice =
            _dataContext.Accommodations
                .Where(i => i.Id == tourDto.AccommodationId)
                .Select(i => i.PricePerNight)
                .FirstOrDefault();

        if (accommodationPrice * (tourDto.EndDate - tourDto.StartDate).Days > tourDto.Price)
        {
            throw new Exception("Стоимость тура не может быть меньше общих затрат на проживание");
        }

        _mapper.Map(tourDto, tour);

        if (!string.IsNullOrEmpty(filePath))
            tour.PhotoPath = filePath;

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tour = await _dataContext.Tours.FirstOrDefaultAsync(r => r.Id == id);
        if (tour == null) throw new Exception("Проживание не найдено");

        _dataContext.Tours.Remove(tour);
        await _dataContext.SaveChangesAsync();
    }
}
