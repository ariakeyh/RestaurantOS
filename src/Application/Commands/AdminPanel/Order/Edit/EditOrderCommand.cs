using Application.Services.OrderServices;
using MediatR;

namespace Application.Commands.AdminPanel.Order.Edit;

public sealed record EditOrderCommand(
    int OrderId,
    string CustomerName,
    string CustomerPhoneNumber,
    ServeInfoDto ServeInfo,
    List<Item> Items) : IRequest;