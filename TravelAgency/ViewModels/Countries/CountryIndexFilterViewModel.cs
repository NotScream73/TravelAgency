using System.ComponentModel.DataAnnotations;

namespace TravelAgency.ViewModels.Countries;

public class CountryIndexFilterViewModel
{
    [Required]
    public string? Name { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
    public int TotalCount { get; set; }
}
