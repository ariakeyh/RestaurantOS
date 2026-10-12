namespace Domain.Entities.Orders;

public class PGPaymentInfo : PaymentInfo
{
    public string ReferenceNo { get; set; }
    public string MaskedCardNo { get; set; }
    public string AdditionalData { get; set; }
}