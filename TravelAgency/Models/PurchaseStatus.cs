using System.ComponentModel;

namespace TravelAgency.Models;

public enum PurchaseStatus
{
    [Description("Заказ создан, ожидается подтверждение")]
    Created,

    [Description("Заказ подтверждён")]
    Confirmed,

    [Description("Заказ ожидает оплаты")]
    PendingPayment,

    [Description("Оплата успешно проведена")]
    Paid,

    [Description("Заказ в процессе выполнения")]
    Processing,

    [Description("Заказ отправлен клиенту")]
    Shipped,

    [Description("Заказ доставлен")]
    Delivered,

    [Description("Заказ отменён")]
    Canceled,

    [Description("Возврат по заказу обработан")]
    Returned,

    [Description("Ошибка в обработке заказа")]
    Error
}
