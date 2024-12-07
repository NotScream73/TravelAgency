using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortDetailsViewModel
{
    public ResortDetailsDTO Item { get; }

    public ResortDetailsViewModel(ResortDetailsDTO item)
    {
        Item = item;
    }
}
