using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper;

public class ResortProfile : Profile
{
    public ResortProfile()
    {
        CreateMap<ResortCreateDTO, Resort>();

        CreateMap<Resort, ResortEditDTO>();
        CreateMap<ResortEditDTO, Resort>();

        CreateMap<Resort, ResortDetailsDTO>();

        CreateMap<Resort, ResortDeleteDTO>();

        CreateMap<Resort, ResortListDTO>();
    }
}
