using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationEditViewModel
{
    public AccommodationEditDTO Item { get; }
    public SelectList TypeOptions { get; }
    public AccommodationEditViewModel(AccommodationEditDTO item, SelectList typeOptions)
    {
        Item = item;
        TypeOptions = typeOptions;
    }
}
