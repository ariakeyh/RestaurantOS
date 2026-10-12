using Application.Common.Dtos;
using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.AdminPanel.Shops.GetCustomers;

public record GetShopCustomersQuery : PaginationRequest<GetShopCustomersResponse>
{
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}

public class GetShopCustomersResponse
{
    public CustomerInfo Customer { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastOrderDate { get; set; }
}