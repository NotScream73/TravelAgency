using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper;

public class AccommodationProfile : Profile
{
    public AccommodationProfile()
    {
        CreateMap<AccommodationCreateDTO, Accommodation>();

        CreateMap<Accommodation, AccommodationEditDTO>();
        CreateMap<AccommodationEditDTO, Accommodation>();

        CreateMap<Accommodation, AccommodationDetailsDTO>();

        CreateMap<Accommodation, AccommodationDeleteDTO>();

        CreateMap<Accommodation, AccommodationListDTO>();
    }
}
