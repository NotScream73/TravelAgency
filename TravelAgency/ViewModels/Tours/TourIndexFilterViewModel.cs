using System.ComponentModel.DataAnnotations;

namespace TravelAgency.ViewModels.Tours;

public class TourIndexFilterViewModel
{
    [Required]
    public string? Name { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
    public int TotalCount { get; set; }
}
