using Application.Common.Dtos;
using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.AdminPanel.Orders.GetAll;

public record GetAllOrdersQuery : PaginationRequest<GetAllOrdersResponse>
{
    public OrderStatus? Status { get; set; }
    public Guid? CustomerId { get; set; }
}

public class GetAllOrdersResponse
{
    public int Id { get; set; }
    public CustomerInfo Customer { get; set; }
    public ServeInfoType Type { get; set; }
    public OrderStatus Status { get; set; }
    public OrderSource Source { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ExpectedPreparation { get; set; }
    public DateTime? ActualPreparation { get; set; }
    public int TotalPrice { get; set; }
    public int TotalPriceWithDiscount { get; set; }
    public string OrderNumber { get; set; }
}