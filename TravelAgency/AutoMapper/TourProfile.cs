using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper;

public class TourProfile : Profile
{
    public TourProfile()
    {
        CreateMap<TourCreateDTO, Tour>()
            .ForMember(entity => entity.PhotoPath, opt => opt.Ignore());

        CreateMap<Tour, TourEditDTO>();

        CreateMap<TourEditDTO, Tour>()
            .ForMember(entity => entity.PhotoPath, opt => opt.Ignore());

        CreateMap<Tour, TourDetailsDTO>();

        CreateMap<Tour, TourDeleteDTO>();

        CreateMap<Tour, TourListDTO>();
    }
}
