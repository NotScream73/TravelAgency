using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Comments;

public class CommentIndexViewModel
{
    public CommentIndexFilterViewModel Filter { get; set; }
    public List<CommentListDTO> List { get; set; }
    public CommentEditDTO Item { get; set; }
    public CommentIndexViewModel(List<CommentListDTO> list, CommentEditDTO item, CommentIndexFilterViewModel filter)
    {
        List = list;
        Item = item;
        Filter = filter;
    }
}
