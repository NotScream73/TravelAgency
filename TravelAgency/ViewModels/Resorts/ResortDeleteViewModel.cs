using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortDeleteViewModel
{
    public ResortDeleteDTO Item { get; set; }

    public ResortDeleteViewModel(ResortDeleteDTO item)
    {
        Item = item;
    }
}
