using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortCreateViewModel
{
    public ResortCreateDTO Item { get; }
    public SelectList TypeOptions { get; }
    public ResortCreateViewModel(ResortCreateDTO item, SelectList typeOptions)
    {
        Item = item;
        TypeOptions = typeOptions;
    }
}
