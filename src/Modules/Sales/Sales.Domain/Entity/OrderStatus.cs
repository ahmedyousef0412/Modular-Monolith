namespace Sales.Domain.Entity;

public enum OrderStatus
{
    Draft,
    PendingPayment,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}
