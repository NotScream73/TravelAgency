using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Tours;

public class TourCreateViewModel
{
    public TourCreateDTO Item { get; }
    public SelectList CountryOptions { get; }
    public SelectList AccommodationOptions { get; }
    public SelectList ResortOptions { get; }
    public TourCreateViewModel(TourCreateDTO item, SelectList countryOptions, SelectList accommodationOptions, SelectList resortOptions)
    {
        Item = item;
        CountryOptions = countryOptions;
        AccommodationOptions = accommodationOptions;
        ResortOptions = resortOptions;
    }
}
