using Application.Common.Dtos;
using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsList;

public sealed record GetCustomerCreditsListQuery(DateTime? From, DateTime? To, DateTime? ExpireFrom, DateTime? ExpireTo,string? CustomerPhoneNumber) : PaginationRequest<GetCustomerCreditsResult>;

public class GetCustomerCreditsResult
{
    public int Id { get; set; }
    public CustomerInfo CustomerInfo { get; set; }
    public int Amount { get; set; }
    public int SpentAmount { get; set; }
    public DateTime? Expiration { get; set; }
    public CustomerCreditType Type { get; set; }
}