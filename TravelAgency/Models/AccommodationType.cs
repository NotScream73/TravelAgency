using System.ComponentModel;

namespace TravelAgency.Models;

public enum AccommodationType
{
    [Description("Не указан")]
    None,
    [Description("Отель")]
    Hotel,
    [Description("Хостел")]
    Hostel,
    [Description("Апартаменты")]
    Apartment,
    [Description("Вилла")]
    Villa,
    [Description("Коттедж")]
    Cottage,
    [Description("Гостевой дом")]
    GuestHouse,
    [Description("Кемпинг")]
    Camping,
    [Description("Мотель")]
    Motel
}