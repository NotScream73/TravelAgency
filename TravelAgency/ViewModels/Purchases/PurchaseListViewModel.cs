using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Purchases;

public class PurchaseListViewModel
{
    public PurchaseIndexFilterViewModel Filter { get; }
    public List<PurchaseListDTO> List { get; }
    public PurchaseListViewModel(PurchaseIndexFilterViewModel filter, List<PurchaseListDTO> list)
    {
        Filter = filter;
        List = list;
    }
}
