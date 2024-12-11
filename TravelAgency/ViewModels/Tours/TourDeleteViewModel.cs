using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Tours;

public class TourDeleteViewModel
{
    public TourDeleteDTO Item { get; set; }

    public TourDeleteViewModel(TourDeleteDTO item)
    {
        Item = item;
    }
}
