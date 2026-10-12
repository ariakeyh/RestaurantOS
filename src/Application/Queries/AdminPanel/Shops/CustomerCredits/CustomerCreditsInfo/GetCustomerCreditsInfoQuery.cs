using System.Text.Json.Serialization;
using MediatR;
using Domain.Enums;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsInfo;

public sealed record GetCustomerCreditsInfoQuery(int Id) : IRequest<List<CustomerCreditDto>>;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(ManualCustomerCreditDto), "0")]
[JsonDerivedType(typeof(OrderCustomerCreditDto), "1")]
public abstract class CustomerCreditDto
{
    public int Id { get; init; }
    public int Amount { get; init; }
    public int SpentAmount { get; init; }
    public DateTime? Expiration { get; init; }
    public CustomerCreditType Type { get; init; }
    public List<SpendHistoryDto> SpendHistory { get; init; }
}

public sealed class ManualCustomerCreditDto : CustomerCreditDto
{
    public string Reason { get; set; }
}

public sealed class OrderCustomerCreditDto : CustomerCreditDto
{
    public OrderInfoDto OrderInfo { get; set; }
    public CashbackInfoDto CashbackInfo { get; set; }
}

public sealed record OrderInfoDto(int Id, DateTime? Date, int TotalPrice);
public sealed record CashbackInfoDto(byte CashbackPercentage, int MaxCashbackAmount, DateTime Creation);

public sealed record SpendHistoryDto(int SpendAmount, int BalanceAmount, OrderInfoDto OrderInfo);