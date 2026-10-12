using Application.Services.OrderServices;
using Domain.Enums;
using MediatR;

namespace Application.Commands.Pos.Orders.Create;

public record CreateOrderCommand(
    string CustomerName,
    string CustomerPhoneNumber,
    OrderSource Source,
    ServeInfoDto ServeInfo,
    PosPaymentInfoDto? PaymentInfo,
    List<Item> Items) : IRequest;