using System.ComponentModel.DataAnnotations;

namespace TravelAgency.ViewModels.Accommodations;

public class AccommodationIndexFilterViewModel
{
    [Required]
    public string? Name { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
    public int TotalCount { get; set; }
}
