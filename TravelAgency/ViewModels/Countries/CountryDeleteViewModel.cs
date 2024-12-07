using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Countries;

public class CountryDeleteViewModel
{
    public CountryDeleteDTO Item { get; set; }

    public CountryDeleteViewModel(CountryDeleteDTO item)
    {
        Item = item;
    }
}
