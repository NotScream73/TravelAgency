using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Tours;

public class TourDetailsViewModel
{
    public TourDetailsDTO Item { get; }

    public TourDetailsViewModel(TourDetailsDTO item)
    {
        Item = item;
    }
}
