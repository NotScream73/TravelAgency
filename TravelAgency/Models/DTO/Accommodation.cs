using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.DTO;

public abstract class AccommodationDTO
{
    [Required(ErrorMessage = "Название не может быть пустым")]
    [DisplayName("Название")]
    public string? Name { get; set; }
}

public class AccommodationCreateDTO : AccommodationDTO
{
    [DisplayName("Описание")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Адрес не может быть пустым")]
    [DisplayName("Адрес")]
    public string? Address { get; set; }

    [DisplayName("Тип проживания")]
    public AccommodationType Type { get; set; }

    [Required(ErrorMessage = "Цена за 1 ночь не может быть пустой")]
    [DisplayName("Цена за 1 ночь, руб")]
    [Range(1, double.MaxValue, ErrorMessage = "Цена за 1 ночь, руб должна быть не меньше 1 и не больше {2}")]
    public decimal PricePerNight { get; set; }
}

public class AccommodationEditDTO : AccommodationDTO
{
    public int Id { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Адрес не может быть пустым")]
    [DisplayName("Адрес")]
    public string? Address { get; set; }

    [DisplayName("Тип проживания")]
    public AccommodationType Type { get; set; }

    [Required(ErrorMessage = "Цена за 1 ночь не может быть пустой")]
    [DisplayName("Цена за 1 ночь, руб")]
    [Range(1, double.MaxValue, ErrorMessage = "Цена за 1 ночь, руб должна быть не меньше 1 и не больше {2}")]
    public decimal PricePerNight { get; set; }
}

public class AccommodationDetailsDTO : AccommodationDTO
{
    public int Id { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }
    [DisplayName("Тип проживания")]
    public AccommodationType Type { get; set; }
    [DisplayName("Адрес")]
    public string? Address { get; set; }
    [DisplayName("Цена за 1 ночь, руб")]
    public decimal PricePerNight { get; set; }
}

public class AccommodationDeleteDTO : AccommodationDTO
{
    public int Id { get; set; }
}

public class AccommodationListDTO : AccommodationDTO
{
    public int Id { get; set; }
    [DisplayName("Тип проживания")]
    public AccommodationType Type { get; set; }
    [DisplayName("Адрес")]
    public string? Address { get; set; }
    [DisplayName("Цена за 1 ночь, руб")]
    public decimal PricePerNight { get; set; }
}