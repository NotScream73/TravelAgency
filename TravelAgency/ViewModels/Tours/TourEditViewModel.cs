using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Tours;

public class TourEditViewModel
{
    public TourEditDTO Item { get; }
    public SelectList CountryOptions { get; }
    public SelectList AccommodationOptions { get; }
    public SelectList ResortOptions { get; }
    public TourEditViewModel(TourEditDTO item, SelectList countryOptions, SelectList accommodationOptions, SelectList resortOptions)
    {
        Item = item;
        CountryOptions = countryOptions;
        AccommodationOptions = accommodationOptions;
        ResortOptions = resortOptions;
    }
}
