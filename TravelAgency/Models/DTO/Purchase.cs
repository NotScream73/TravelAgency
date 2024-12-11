using System.ComponentModel;

namespace TravelAgency.Models.DTO;

public abstract class PurchaseDTO
{
}

public class PurchaseListDTO : PurchaseDTO
{
    [DisplayName("#")]
    public int Id { get; set; }
    [DisplayName("Общая стоимость")]
    public decimal TotalPrice { get; set; }
    [DisplayName("Количество товара")]
    public int ProductCount { get; set; }
    [DisplayName("Статус")]
    public PurchaseStatus Status { get; set; }
    [DisplayName("Создан")]
    public DateTimeOffset Created { get; set; }
}

public class PurchaseIndexDTO
{
    [DisplayName("#")]
    public int Id { get; set; }
    [DisplayName("Название тура")]
    public string TourName { get; set; }
    [DisplayName("Стоимость тура")]
    public decimal Price { get; set; }
    [DisplayName("Количество билетов")]
    public int ProductCount { get; set; }
}

public class PurchaseDetailsDTO
{
    [DisplayName("#")]
    public int Id { get; set; }
    [DisplayName("Название тура")]
    public string TourName { get; set; }
    [DisplayName("Стоимость тура")]
    public decimal Price { get; set; }
    [DisplayName("Количество билетов")]
    public int ProductCount { get; set; }
    public string PhotoPath { get; set; }
}

public class PurchaseDetailsItemDTO
{
    [DisplayName("№ заказа")]
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    [DisplayName("Статус")]
    public PurchaseStatus Status { get; set; }
    [DisplayName("Создан")]
    public DateTimeOffset Created { get; set; }
}