using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationDetailsViewModel
{
    public AccommodationDetailsDTO Item { get; }

    public AccommodationDetailsViewModel(AccommodationDetailsDTO item)
    {
        Item = item;
    }
}
