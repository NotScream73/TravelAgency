using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Countries;

public class CountryEditViewModel
{
    public CountryEditDTO Item { get; }
    public CountryEditViewModel(CountryEditDTO item)
    {
        Item = item;
    }
}
