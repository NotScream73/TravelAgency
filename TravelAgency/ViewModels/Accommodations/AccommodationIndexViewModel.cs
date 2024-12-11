using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationIndexViewModel
{
    public AccommodationIndexFilterViewModel Filter { get; }
    public List<AccommodationListDTO> List { get; }
    public AccommodationIndexViewModel(AccommodationIndexFilterViewModel filter, List<AccommodationListDTO> list)
    {
        Filter = filter;
        List = list;
    }
}
