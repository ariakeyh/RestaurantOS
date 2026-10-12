using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class PosPaymentInfoDto : PaymentInfoDto
{
    public string TerminalNo { get; set; }
    public string AcceptorCode { get; set; }
    public string TraceNo { get; set; }
    public string MaskedCardNo { get; set; }
    public override PaymentInfo MapToModel()
    {
        return new PosPaymentInfo
        {
            Amount = Amount,
            PaidAt = PaidAt,
            Type = PaymentInfoType.POS,
            TerminalNo = TerminalNo,
            AcceptorCode = AcceptorCode,
            TraceNo = TraceNo,
            MaskedCardNo = MaskedCardNo
        };
    }
}