namespace Domain.Entities.Orders;

public abstract class AmendablePaymentInfo : PaymentInfo
{
    public string Reason { get; set; }
}