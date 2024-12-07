using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortIndexViewModel
{
    public ResortIndexFilterViewModel Filter { get; }
    public List<ResortListDTO> List { get; }
    public ResortIndexViewModel(ResortIndexFilterViewModel filter, List<ResortListDTO> list)
    {
        Filter = filter;
        List = list;
    }
}
