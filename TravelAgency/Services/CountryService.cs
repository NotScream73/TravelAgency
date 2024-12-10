using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Countries;

namespace TravelAgency.Services;

public class CountryService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public CountryService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<List<CountryForSelectDTO>> GetAllForSelectAsync()
    {
        return
            await _dataContext.Countries
                .Select(i => new CountryForSelectDTO
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .OrderBy(i => i.Name)
                .ToListAsync();
    }

    public async Task<(List<CountryListDTO> list, int totalCount)> GetAllAsync(CountryIndexFilterViewModel filter)
    {
        var query =
            _dataContext.Countries
                .ProjectTo<CountryListDTO>(_mapper.ConfigurationProvider);

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

    public async Task<CountryDetailsDTO> GetByIdAsync(int id)
    {
        var Country = await _dataContext.Countries.ProjectTo<CountryDetailsDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(i => i.Id == id);

        if (Country == null) throw new Exception("Страна не найдена");

        return Country;
    }

    public CountryCreateDTO GetForCreate()
    {
        return new CountryCreateDTO();
    }

    public async Task<CountryEditDTO> GetForEditAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Страна не найдена");
        }

        var Country = await _dataContext.Countries.ProjectTo<CountryEditDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (Country == null) throw new Exception("Страна не найдена");

        return Country;
    }

    public async Task<CountryDeleteDTO> GetForDeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("Страна не найдена");
        }

        var Country = await _dataContext.Countries.ProjectTo<CountryDeleteDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

        if (Country == null) throw new Exception("Страна не найдена");

        return Country;
    }

    public async Task CreateAsync(CountryCreateDTO CountryDto)
    {
        var Country = _mapper.Map<Country>(CountryDto);

        await _dataContext.Countries.AddAsync(Country);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(CountryEditDTO CountryDto)
    {
        var Country = await _dataContext.Countries.FirstOrDefaultAsync(r => r.Id == CountryDto.Id);
        if (Country == null) throw new Exception("Страна не найдена");

        _mapper.Map(CountryDto, Country);

        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Country = await _dataContext.Countries.FirstOrDefaultAsync(r => r.Id == id);
        if (Country == null) throw new Exception("Страна не найдена");

        _dataContext.Countries.Remove(Country);
        await _dataContext.SaveChangesAsync();
    }
}
