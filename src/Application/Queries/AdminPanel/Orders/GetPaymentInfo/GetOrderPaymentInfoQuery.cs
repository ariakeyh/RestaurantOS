using Application.Services.OrderServices;
using MediatR;

namespace Application.Queries.AdminPanel.Orders.GetPaymentInfo;

public class GetOrderPaymentInfoQuery : IRequest<List<PaymentInfoDto>>
{
    public int OrderId { get; set; }
}