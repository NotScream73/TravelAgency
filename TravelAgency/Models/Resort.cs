namespace TravelAgency.Models
{
    public class Resort
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ResortType ResortType { get; set; } = ResortType.None;
        public int CountryId { get; set; }
    }
}
