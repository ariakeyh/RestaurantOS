using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class OnSiteServeInfoDto : ServeInfoDto
{
    public byte TableNumber { get; set; }
    public override OrderServeInfo MapToModel()
    {
        return new OnSiteOrderServeInfo
        {
            Type = ServeInfoType.OnSite,
            TableNumber = TableNumber
        };
    }
}