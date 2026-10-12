using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class CashPaymentInfoDto : PaymentInfoDto
{
    public override PaymentInfo MapToModel()
    {
        return new CashPaymentInfo
        {
            Amount = Amount,
            PaidAt = PaidAt,
            Type = PaymentInfoType.Cash
        };
    }
}