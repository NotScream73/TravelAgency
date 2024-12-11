using AutoMapper;
using TravelAgency.Models;
using TravelAgency.Models.DTO;

namespace TravelAgency.AutoMapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentListDTO>()
            .ForMember(dto => dto.TourName, entity => entity.MapFrom(m => m.Tour.Name))
            .ForMember(dto => dto.UserName, entity => entity.MapFrom(m => m.User.UserName));
        CreateMap<Comment, CommentEditDTO>()
            .ForMember(dto => dto.TourName, entity => entity.MapFrom(m => m.Tour.Name));
    }
}
