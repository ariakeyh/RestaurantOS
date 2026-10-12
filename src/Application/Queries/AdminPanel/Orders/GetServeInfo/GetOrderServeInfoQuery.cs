using Application.Services.OrderServices;
using MediatR;

namespace Application.Queries.AdminPanel.Orders.GetServeInfo;

public class GetOrderServeInfoQuery : IRequest<ServeInfoDto>
{
    public int OrderId { get; set; }
}