using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Purchases;

public class PurchaseDetailsViewModel
{
    public PurchaseDetailsItemDTO Item { get; set; }
    public List<PurchaseDetailsDTO> TourPurchaseList { get; }

    public PurchaseDetailsViewModel(PurchaseDetailsItemDTO item, List<PurchaseDetailsDTO> tourPurchaseList)
    {
        Item = item;
        TourPurchaseList = tourPurchaseList;
    }
}
