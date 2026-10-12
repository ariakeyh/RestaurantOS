using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class TransferPaymentInfoDto : PaymentInfoDto
{
    public string DestinationCardNo { get; set; }
    public string ReferenceNo { get; set; }
    public override PaymentInfo MapToModel()
    {
        return new TransferPaymentInfo
        {
            Amount = Amount,
            PaidAt = PaidAt,
            Type = PaymentInfoType.Transfer,
            DestinationCardNo = DestinationCardNo,
            ReferenceNo = ReferenceNo
        };
    }
}