using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Countries;

public class CountryDetailsViewModel
{
    public CountryDetailsDTO Item { get; }

    public CountryDetailsViewModel(CountryDetailsDTO item)
    {
        Item = item;
    }
}
