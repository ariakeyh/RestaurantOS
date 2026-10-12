namespace Domain.Enums;

public enum OrderStatus : byte
{
    PendingPayment = 1,
    PendingAcceptance = 2,
    Preparing = 10,
    Completed = 20,
    Cancelled = 21,
    Refunded = 22
}