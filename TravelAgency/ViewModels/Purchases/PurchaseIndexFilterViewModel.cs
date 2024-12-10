namespace TravelAgency.ViewModels.Purchases;

public class PurchaseIndexFilterViewModel
{
    public string? UserId { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
    public int TotalCount { get; set; }
}
