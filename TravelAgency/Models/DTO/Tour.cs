using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.DTO;

public abstract class TourDTO
{
    [DisplayName("Название")]
    [Required(ErrorMessage = "Название не может быть пустым")]
    public string? Name { get; set; }
}

public class TourCreateDTO : TourDTO
{
    [DisplayName("Описание")]
    public string? Description { get; set; }

    [DisplayName("Стоимость тура")]
    [Required(ErrorMessage = "Стоимость не может быть пустой")]
    [Range(1, double.MaxValue, ErrorMessage = "Стоимость, руб должна быть не меньше 1 и не больше {2}")]
    public decimal Price { get; set; }

    [DisplayName("Количество билетов")]
    [Required(ErrorMessage = "Количество билетов не может быть пустым")]
    [Range(1, int.MaxValue, ErrorMessage = "Количество билетов должно быть не меньше 1 и не больше {2}")]
    public int Count { get; set; }

    [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
    [DisplayName("Дата начала")]
    [Required(ErrorMessage = "Дата начала не может быть пустой")]
    public DateTime StartDate { get; set; }

    [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
    [DisplayName("Дата окончания")]
    [Required(ErrorMessage = "Дата окончания не может быть пустой")]
    public DateTime EndDate { get; set; }

    [DisplayName("Страна")]
    public int CountryId { get; set; }

    [DisplayName("Курорт")]
    public int ResortId { get; set; }

    [DisplayName("Место проживания")]
    public int AccommodationId { get; set; }
}

public class TourEditDTO : TourDTO
{
    public int Id { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }

    [DisplayName("Стоимость тура")]
    [Required(ErrorMessage = "Стоимость не может быть пустой")]
    [Range(1, double.MaxValue, ErrorMessage = "Стоимость, руб должна быть не меньше 1 и не больше {2}")]
    public decimal Price { get; set; }

    [DisplayName("Количество билетов")]
    [Required(ErrorMessage = "Количество билетов не может быть пустым")]
    [Range(1, int.MaxValue, ErrorMessage = "Количество билетов должно быть не меньше 1 и не больше {2}")]
    public int Count { get; set; }

    [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
    [DisplayName("Дата начала")]
    [Required(ErrorMessage = "Дата начала не может быть пустой")]
    public DateTime StartDate { get; set; }

    [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
    [DisplayName("Дата окончания")]
    [Required(ErrorMessage = "Дата окончания не может быть пустой")]
    public DateTime EndDate { get; set; }

    [DisplayName("Страна")]
    public int CountryId { get; set; }

    [DisplayName("Курорт")]
    public int ResortId { get; set; }

    [DisplayName("Место проживания")]
    public int AccommodationId { get; set; }
}

public class TourDetailsDTO : TourDTO
{
    public int Id { get; set; }

    [DisplayName("Описание")]
    public string? Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [DisplayName("Стоимость тура")]
    public decimal Price { get; set; }

    [DisplayName("Количество билетов")]
    [Required(ErrorMessage = "Количество билетов не может быть пустым")]
    public int Count { get; set; }

    [DisplayName("Период")]
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string PhotoPath { get; set; }

    [DisplayName("Страна")]
    public int CountryId { get; set; }

    [DisplayName("Курорт")]
    public int ResortId { get; set; }

    [DisplayName("Место проживания")]
    public string AccommodationName { get; set; }

    [DisplayName("Стоимость проживания за 1 ночь")]
    public decimal AccommodationPricePerNight { get; set; }

    [DisplayName("Адрес проживания")]
    public string AccommodationAddress { get; set; }

    [DisplayName("Курорт")]
    public string ResortName { get; set; }

    [DisplayName("Страна")]
    public string CountryName { get; set; }
}

public class TourDeleteDTO : TourDTO
{
    public int Id { get; set; }
}

public class TourListDTO : TourDTO
{
    public int Id { get; set; }
    public string PhotoPath { get; set; }
    [DisplayName("Период")]
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [DisplayName("Курорт")]
    public string ResortName { get; set; }

    [DisplayName("Страна")]
    public string CountryName { get; set; }
    [DisplayFormat(DataFormatString = "{0:N2}")]
    [DisplayName("Стоимость тура")]
    public decimal Price { get; set; }
    [DisplayName("Количество билетов")]
    public int Count { get; set; }
    public double Score { get; set; }
}