using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.Customer.Orders.GetOrders;

public record GetOrdersQuery : PaginationRequest<GetOrdersQueryResult>;

public record GetOrdersQueryResult(
    int OrderId,
    string OrderNumber,
    int TotalPrice,
    int TotalPriceWithDiscount,
    ServeInfoType ServeInfoType,
    List<OrderItemDto> Items
);

public record OrderItemDto(
    int MenuItemId,
    int Quantity,
    int Price,
    int PriceWithDiscount,
    string ImageName,
    string Name
);