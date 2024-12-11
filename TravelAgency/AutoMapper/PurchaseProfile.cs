using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Purchase, PurchaseDetailsItemDTO>();
        }
    }
}
