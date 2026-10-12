using System.Text.Json.Serialization;
using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "source")]
[JsonDerivedType(typeof(CashierSourceInfoDto), nameof(OrderSource.Cashier))]
[JsonDerivedType(typeof(PosSourceInfoDto), nameof(OrderSource.POS))]
[JsonDerivedType(typeof(WebsiteSourceInfoDto), nameof(OrderSource.Website))]
public abstract class SourceInfoDto
{
    public static SourceInfoDto FromModel(Order model) => model.Source switch
    {
        OrderSource.Cashier => new CashierSourceInfoDto
        {
            CashierId = ((CashierOrder)model).CashierId,
            CashierName = ((CashierOrder)model).Cashier.Name
        },
        OrderSource.POS => new PosSourceInfoDto
        {
            PosId = ((PosOrder)model).POSDeviceId,
            PosName = ((PosOrder)model).POSDevice.User.Name
        },
        OrderSource.Website => new WebsiteSourceInfoDto(),
        _ => throw new ArgumentOutOfRangeException(nameof(model.Source))
    };
}