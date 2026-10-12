using Application.Services.OrderServices;
using MediatR;

namespace Application.Queries.AdminPanel.Orders.GetSourceInfo;

public class GetOrderSourceInfoQuery : IRequest<SourceInfoDto>
{
    public int OrderId { get; set; }
}