using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

public class DeliveryServeInfoDto : ServeInfoDto
{
    public string Address { get; set; }
    public override OrderServeInfo MapToModel()
    {
        return new DeliveryOrderServeInfo
        {
            Type = ServeInfoType.Delivery,
            Address = Address
        };
    }
}