namespace Domain.Entities.Orders;

public class TransferPaymentInfo : AmendablePaymentInfo
{
    public string DestinationCardNo { get; set; }
    public string ReferenceNo { get; set; }
}