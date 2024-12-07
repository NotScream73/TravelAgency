using TravelAgency.Models.DTO;

namespace TravelAgency.ViewModels.Resorts;

public class ResortIndexViewModel
{
    public ResortIndexFilterViewModel Filter { get; }
    public List<ResortListDTO> ResortList { get; }
    public ResortIndexViewModel(ResortIndexFilterViewModel filter, List<ResortListDTO> resortList)
    {
        Filter = filter;
        ResortList = resortList;
    }
}
