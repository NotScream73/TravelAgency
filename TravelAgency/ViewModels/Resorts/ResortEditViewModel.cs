using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortEditViewModel
{
    public ResortEditDTO Item { get; }
    public SelectList TypeOptions { get; }
    public ResortEditViewModel(ResortEditDTO item, SelectList typeOptions)
    {
        Item = item;
        TypeOptions = typeOptions;
    }
}
