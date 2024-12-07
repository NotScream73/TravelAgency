using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Resorts;

namespace TravelAgency.Services;

public class ResortService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ResortService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<(List<ResortListDTO> list, int totalCount)> GetAllAsync(ResortIndexFilterViewModel filter)
    {
        var query =
            _dataContext.Resorts
                .ProjectTo<ResortListDTO>(_mapper.ConfigurationProvider);

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

    public async Task<ResortDetailsDTO> GetByIdAsync(int id)
    {
        var resort = await _dataContext.Resorts.ProjectTo<ResortDetailsDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(i => i.Id == id);

        if (resort == null) throw new Exception("Курорт не найден");

        return resort;
    }

    public ResortCreateDTO GetForCreate()
    {
        return new ResortCreateDTO
        {
            Type = ResortType.None
        };
    }

    public async Task<ResortEditDTO> GetForEditAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Курорт не найден");
        }

        var resort = await _dataContext.Resorts.ProjectTo<ResortEditDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (resort == null) throw new Exception("Курорт не найден");

        return resort;
    }

    public async Task<ResortDeleteDTO> GetForDeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Курорт не найден");
        }

        var resort = await _dataContext.Resorts.ProjectTo<ResortDeleteDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (resort == null) throw new Exception("Курорт не найден");

        return resort;
    }

    public async Task CreateAsync(ResortCreateDTO resortDto)
    {
        var resort = _mapper.Map<Resort>(resortDto);

        await _dataContext.Resorts.AddAsync(resort);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ResortEditDTO resortDto)
    {
        var resort = await _dataContext.Resorts.FirstOrDefaultAsync(r => r.Id == resortDto.Id);
        if (resort == null) throw new Exception("Курорт не найден");

        _mapper.Map(resortDto, resort);

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var resort = await _dataContext.Resorts.FirstOrDefaultAsync(r => r.Id == id);
        if (resort == null) throw new Exception("Курорт не найден");

        _dataContext.Resorts.Remove(resort);
        await _dataContext.SaveChangesAsync();
    }

}
