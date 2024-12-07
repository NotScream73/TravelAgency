using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Accommodations;

namespace TravelAgency.Services
{
    public class AccommodationService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AccommodationService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<(List<AccommodationListDTO> list, int totalCount)> GetAllAsync(AccommodationIndexFilterViewModel filter)
        {
            var query =
                _dataContext.Accommodations
                    .ProjectTo<AccommodationListDTO>(_mapper.ConfigurationProvider);

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

        public async Task<AccommodationDetailsDTO> GetByIdAsync(int id)
        {
            var accommodation = await _dataContext.Accommodations.ProjectTo<AccommodationDetailsDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(i => i.Id == id);

            if (accommodation == null) throw new Exception("Проживание не найдено");

            return accommodation;
        }

        public AccommodationCreateDTO GetForCreate()
        {
            return new AccommodationCreateDTO();
        }

        public async Task<AccommodationEditDTO> GetForEditAsync(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Проживание не найдено");
            }

            var Accommodation = await _dataContext.Accommodations.ProjectTo<AccommodationEditDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

            if (Accommodation == null) throw new Exception("Проживание не найдено");

            return Accommodation;
        }

        public async Task<AccommodationDeleteDTO> GetForDeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Проживание не найдено");
            }

            var accommodation = await _dataContext.Accommodations.ProjectTo<AccommodationDeleteDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);

            if (accommodation == null) throw new Exception("Проживание не найдено");

            return accommodation;
        }

        public async Task CreateAsync(AccommodationCreateDTO accommodationDto)
        {
            var accommodation = _mapper.Map<Accommodation>(accommodationDto);

            await _dataContext.Accommodations.AddAsync(accommodation);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccommodationEditDTO accommodationDto)
        {
            var accommodation = await _dataContext.Accommodations.FirstOrDefaultAsync(r => r.Id == accommodationDto.Id);
            if (accommodation == null) throw new Exception("Проживание не найдено");

            _mapper.Map(accommodationDto, accommodation);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var accommodation = await _dataContext.Accommodations.FirstOrDefaultAsync(r => r.Id == id);
            if (accommodation == null) throw new Exception("Проживание не найдено");

            _dataContext.Accommodations.Remove(accommodation);
            await _dataContext.SaveChangesAsync();
        }
    }
}
