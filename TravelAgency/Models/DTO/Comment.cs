using System.ComponentModel;

namespace TravelAgency.Models.DTO;

public abstract class CommentDTO
{
}

public class CommentListDTO
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public string TourName { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public int Score { get; set; }
}

public class CommentEditDTO
{
    [DisplayName("Комментарий")]
    public string Message { get; set; }
    public string? Description { get; set; }
    public int Score { get; set; }
    public string? TourName { get; set; }
    public int TourId { get; set; }
}