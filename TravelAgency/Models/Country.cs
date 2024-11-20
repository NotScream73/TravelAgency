using System.ComponentModel;

namespace TravelAgency.Models
{
    public class Country
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; } = string.Empty;
    }
}
