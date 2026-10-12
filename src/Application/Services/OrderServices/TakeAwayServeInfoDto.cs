using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class TakeAwayServeInfoDto : ServeInfoDto
{
    public override OrderServeInfo MapToModel()
    {
        return new TakeAwayOrderServeInfo
        {
            Type = ServeInfoType.TakeAway
        };
    }
}