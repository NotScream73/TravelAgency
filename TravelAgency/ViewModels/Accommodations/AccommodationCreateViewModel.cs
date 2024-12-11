using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationCreateViewModel
{
    public AccommodationCreateDTO Item { get; }
    public SelectList TypeOptions { get; }
    public AccommodationCreateViewModel(AccommodationCreateDTO item, SelectList typeOptions)
    {
        Item = item;
        TypeOptions = typeOptions;
    }
}
