using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationDeleteViewModel
{
    public AccommodationDeleteDTO Item { get; set; }

    public AccommodationDeleteViewModel(AccommodationDeleteDTO item)
    {
        Item = item;
    }
}
