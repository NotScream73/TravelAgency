using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryCreateDTO, Country>();

        CreateMap<Country, CountryEditDTO>();
        CreateMap<CountryEditDTO, Country>();

        CreateMap<Country, CountryDetailsDTO>();

        CreateMap<Country, CountryDeleteDTO>();

        CreateMap<Country, CountryListDTO>();
    }
}
