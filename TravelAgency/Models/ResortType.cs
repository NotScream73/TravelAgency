using System.ComponentModel;

namespace TravelAgency.Models;

public enum ResortType
{
    [Description("Не указан")]
    None,
    [Description("Пляжный курорт")]
    Beach,
    [Description("Горнолыжный курорт")]
    Ski,
    [Description("СПА-курорт")]
    Spa,
    [Description("Эко-курорт")]
    Eco,
    [Description("Приключенческий курорт")]
    Adventure,
    [Description("Культурный курорт")]
    Cultural,
    [Description("Семейный курорт")]
    Family,
    [Description("Городской курорт")]
    Urban,
    [Description("Островной курорт")]
    Island,
    [Description("Пустынный курорт")]
    Desert
}