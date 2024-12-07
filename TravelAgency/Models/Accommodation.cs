using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Accommodation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string? Description { get; set; }
        [Column("address")]
        public string Address { get; set; } = string.Empty;
        [Column("type")]
        public AccommodationType Type { get; set; }
        [Column("price_per_night")]
        public decimal PricePerNight { get; set; }
    }
}
