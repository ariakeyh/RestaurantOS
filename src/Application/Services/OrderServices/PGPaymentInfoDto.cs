using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class PGPaymentInfoDto : PaymentInfoDto
{
    public string ReferenceNo { get; set; }
    public string MaskedCardNo { get; set; }
    public string AdditionalData { get; set; }
    public override PaymentInfo MapToModel()
    {
        return new PGPaymentInfo
        {
            Amount = Amount,
            PaidAt = PaidAt,
            Type = PaymentInfoType.PG,
            ReferenceNo = ReferenceNo,
            MaskedCardNo = MaskedCardNo,
            AdditionalData = AdditionalData
        };
    }
}