using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.GetCustomerProfile;

public class GetCustomerProfileQueryHandler(
    IAppDbContext context,
    IAuthService authService
) : IRequestHandler<GetCustomerProfileQuery, GetCustomerProfileResult>
{
    public async Task<GetCustomerProfileResult> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        //Pull core customer info + orders + items
        var customerData = await context.Customers
            .Where(c => c.Id == request.Id && c.ShopId == shopId)
            .Select(c => new
            {
                c.User.Name,
                c.User.PhoneNumber,
                c.Badge,
                RegistrationDate = c.CreatedAt,
                Orders = c.Orders.Select(o => new
                {
                    o.TotalPriceWithDiscount,
                    CreationDate = o.CreatedAt,
                    Items = o.Items.Select(i => new { i.MenuItemId, i.Quantity })
                })
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (customerData is null)
            throw new NotFoundException(ErrorMessages.NotFound);
        
        var orders = customerData.Orders.ToList();

        var totalOrders = orders.Count;
        var totalSpend = orders.Sum(o => (decimal)o.TotalPriceWithDiscount);
        var lastOrderDate = orders.Max(o => o.CreationDate);

        // Average order interval
        var avgInterval = TimeSpan.Zero;
        if (orders.Count > 1)
        {
            var orderedDates = orders
                .Select(o => o.CreationDate)
                .OrderBy(d => d)
                .ToList();

            var intervals = orderedDates
                .Zip(orderedDates.Skip(1), (prev, next) => next - prev)
                .ToList();

            avgInterval = TimeSpan.FromTicks((long)intervals.Average(i => i.Ticks));
        }

        // Top 3 items
        var topItems = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.MenuItemId)
            .OrderByDescending(g => g.Sum(i => i.Quantity))
            .Take(3)
            .Select(g => new FavoriteMenuItem(g.Key))
            .ToList();

        
        return new GetCustomerProfileResult(
            customerData.Name,
            customerData.PhoneNumber,
            customerData.Badge,
            customerData.RegistrationDate,
            totalOrders,
            totalSpend,
            lastOrderDate,
            avgInterval,
            topItems
        );
    }
}