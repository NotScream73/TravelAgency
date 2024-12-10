using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models;

public class Tour
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("count")]
    public int Count { get; set; }
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    [Column("country_id")]
    public int CountryId { get; set; }
    public Country Country { get; set; }
    [Column("resort_id")]
    public int ResortId { get; set; }
    public Resort Resort { get; set; }
    [Column("accommodation_id")]
    public int AccommodationId { get; set; }
    public Accommodation Accommodation { get; set; }
    [Column("photo_path")]
    public string PhotoPath { get; set; }
}
