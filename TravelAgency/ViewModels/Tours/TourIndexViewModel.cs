using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Tours;

public class TourIndexViewModel
{
    public TourIndexFilterViewModel Filter { get; }
    public List<TourListDTO> List { get; }
    public TourIndexViewModel(TourIndexFilterViewModel filter, List<TourListDTO> list)
    {
        Filter = filter;
        List = list;
    }
}
