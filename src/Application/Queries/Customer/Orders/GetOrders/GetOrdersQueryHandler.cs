using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Customer.Orders.GetOrders;

public class GetOrdersQueryHandler(IAppDbContext context, IAuthService authService,IObjectStorage objectStorage)
    : IRequestHandler<GetOrdersQuery, Pagination<GetOrdersQueryResult>>

{
    public async Task<Pagination<GetOrdersQueryResult>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var userId = authService.GetUserId();

        var rawResults = await context.Orders.AsNoTrackingWithIdentityResolution()
            .Where(x => x.Customer.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new {
                x.Id,
                x.TotalPrice,
                x.TotalPriceWithDiscount,
                x.OrderNumber,
                x.ServeInfo.Type,
                Items = x.Items.Select(i => new {
                    i.Id,
                    i.Quantity,
                    i.Price,
                    i.PriceWithDiscount,
                    i.MenuItem.ImageName,
                    i.MenuItem.Name,
                }).ToList()
            })
            .ToPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);

        var finalOrders = await Task.WhenAll(rawResults.Items.Select(async order =>
            new GetOrdersQueryResult(
                order.Id,
                order.OrderNumber,
                order.TotalPrice,
                order.TotalPriceWithDiscount,
                order.Type,
                (await Task.WhenAll(order.Items.Select(async i =>
                    new OrderItemDto(
                        i.Id,
                        i.Quantity,
                        i.Price,
                        i.PriceWithDiscount,
                        await objectStorage.GetFileUrl(i.Name),
                        i.Name
                    )
                ))).ToList()
            )
        ));

        return new Pagination<GetOrdersQueryResult>
        {
            Items = finalOrders.ToList(),
            TotalItems = rawResults.TotalItems,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}