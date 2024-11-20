namespace TravelAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public int CountryId { get; set; }
        public int ResortId { get; set; }
        public int AccommodationId { get; set; }
    }
}
