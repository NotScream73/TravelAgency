using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public enum ResortType
    {
        [Display(Name = "Не указан")]
        None = 0,
        [Display(Name = "Пляжный курорт")]
        Beach = 1,
        [Display(Name = "Горнолыжный курорт")]
        Ski = 2,
        [Display(Name = "СПА-курорт")]
        Spa = 3,
        [Display(Name = "Экокурорт")]
        EcoTourism = 4,
        [Display(Name = "Курорт для активного отдыха")]
        Adventure = 5,
        [Display(Name = "Семейный курорт")]
        Family = 6,
        [Display(Name = "Культурный и исторический курорт")]
        Cultural = 7,
        [Display(Name = "Оздоровительный курорт")]
        Wellness = 8,
        [Display(Name = "Гольф-курорт")]
        Golf = 9,
        [Display(Name = "Круизный курорт")]
        Cruise = 10
    }
}
