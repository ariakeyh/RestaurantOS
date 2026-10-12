using System.Text.Json.Serialization;
using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TakeAwayServeInfoDto), nameof(ServeInfoType.TakeAway))]
[JsonDerivedType(typeof(OnSiteServeInfoDto), nameof(ServeInfoType.OnSite))]
[JsonDerivedType(typeof(DeliveryServeInfoDto), nameof(ServeInfoType.Delivery))]
public abstract class ServeInfoDto
{
    public abstract OrderServeInfo MapToModel();
    
    public static ServeInfoDto FromModel(OrderServeInfo model) => model.Type switch
    {
        ServeInfoType.OnSite => new OnSiteServeInfoDto { TableNumber = ((OnSiteOrderServeInfo)model).TableNumber },
        ServeInfoType.Delivery => new DeliveryServeInfoDto { Address = ((DeliveryOrderServeInfo)model).Address },
        ServeInfoType.TakeAway => new TakeAwayServeInfoDto(),
        _ => throw new ArgumentOutOfRangeException(nameof(model.Type))
    };
}