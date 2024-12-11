using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.ViewModels.Comments;

namespace TravelAgency.Services;

public class CommentService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public CommentService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<(CommentEditDTO item, List<CommentListDTO> list)> GetForIndexAsync(CommentIndexFilterViewModel filter, string userId)
    {
        var query =
            _dataContext.Comments
                .Where(c => c.TourId == filter.TourId)
                .ProjectTo<CommentListDTO>(_mapper.ConfigurationProvider);

        var item = new CommentEditDTO();

        filter.TotalCount = query.Count();

        query = query
            .Skip(filter.Page * filter.Size)
            .Take(filter.Size);

        var list = await query.OrderByDescending(i => i.Id).ToListAsync();

        return (item, list);
    }

    public async Task CreateAsync(CommentEditDTO commentDTO, string userId)
    {
        var comment = new Comment
        {
            Message = commentDTO.Message,
            Score = commentDTO.Score,
            TourId = commentDTO.TourId,
            UserId = userId
        };

        await _dataContext.Comments.AddAsync(comment);

        await _dataContext.SaveChangesAsync();
    }
}
