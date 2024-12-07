using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Countries;

public class CountryIndexViewModel
{
    public CountryIndexFilterViewModel Filter { get; }
    public List<CountryListDTO> List { get; }
    public CountryIndexViewModel(CountryIndexFilterViewModel filter, List<CountryListDTO> list)
    {
        Filter = filter;
        List = list;
    }
}
