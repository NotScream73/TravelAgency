using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class Purchase
{
    [Column("id")]
    public int Id { get; set; }
    [Column("total_price")]
    public decimal TotalPrice { get; set; }
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("status")]
    public PurchaseStatus Status { get; set; }
    [Column("created")]
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
}
