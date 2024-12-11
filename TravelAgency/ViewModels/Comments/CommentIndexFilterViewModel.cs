namespace TravelAgency.ViewModels.Comments;

public class CommentIndexFilterViewModel
{
    public int TourId { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
    public int TotalCount { get; set; }
}
