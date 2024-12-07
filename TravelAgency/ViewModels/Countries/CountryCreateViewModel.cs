using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Countries;

public class CountryCreateViewModel
{
    public CountryCreateDTO Item { get; }
    public CountryCreateViewModel(CountryCreateDTO item)
    {
        Item = item;
    }
}
