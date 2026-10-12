using Application.Services.OrderServices;
using MediatR;
using Domain.Enums;

namespace Application.Commands.AdminPanel.Order.Create;

public record CreateOrderCommand(
    string CustomerName,
    string CustomerPhoneNumber,
    OrderSource Source,
    ServeInfoDto ServeInfo,
    PaymentInfoDto? PaymentInfo,
    List<Item> Items) : IRequest;