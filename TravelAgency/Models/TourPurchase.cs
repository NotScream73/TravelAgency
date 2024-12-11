using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class TourPurchase
{
    [Column("id")]
    public int Id { get; set; }
    [Column("tour_id")]
    public int TourId { get; set; }
    [Column("purchase_id")]
    public int PurchaseId { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("count")]
    public int Count { get; set; }
}
