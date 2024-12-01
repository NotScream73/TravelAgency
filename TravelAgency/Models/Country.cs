using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class Country
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
}
