using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class Comment
{
    [Column("id")]
    public int Id { get; set; }
    [Column("tour_id")]
    public int TourId { get; set; }
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("message")]
    public string Message { get; set; }
    [Column("score")]
    public int Score { get; set; }
    public Tour Tour { get; set; }
    public User User { get; set; }
}
