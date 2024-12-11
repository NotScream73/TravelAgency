using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Purchases;

public class PurchaseIndexViewModel
{
    public List<PurchaseIndexDTO> List { get; }
    public PurchaseIndexViewModel(List<PurchaseIndexDTO> list)
    {
        List = list;
    }
}
