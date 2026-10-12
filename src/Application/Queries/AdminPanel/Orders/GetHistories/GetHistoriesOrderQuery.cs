using Application.Common.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Orders.GetHistories;

public record GetHistoriesOrderQuery : IRequest<List<GetHistoriesOrderResponse>>
{
    public int OrderId { get; set; }
}

public class GetHistoriesOrderResponse
{
    public OrderStatus? Status { get; set; }
    public DateTime Date { get; set; }
    public CustomerInfo Creator { get; set; }
}