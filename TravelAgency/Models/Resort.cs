using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class Resort
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("type")]
    public ResortType Type { get; set; }
}
