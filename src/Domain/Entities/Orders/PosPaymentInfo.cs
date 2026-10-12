namespace Domain.Entities.Orders;

public class PosPaymentInfo : PaymentInfo
{
    public string TerminalNo { get; set; }
    public string AcceptorCode { get; set; }
    public string TraceNo { get; set; }
    public string MaskedCardNo { get; set; }
}