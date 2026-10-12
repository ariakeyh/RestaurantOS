using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CustomerService (UserService userService, IAppDbContext context)
{
    public async Task<Customer> GetCustomer(int userId, int shopId, CancellationToken cancellationToken)
    {
        var customer = await context.Customers
            .Where(x => x.UserId == userId && x.ShopId == shopId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(customer != null)
            return customer;
        
        var userExists = await context.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        if (!userExists)
            throw new NotFoundException(ErrorMessages.User_NotFound);

        var shopInfo = await context.Shops.FirstOrDefaultAsync(x => x.Id == shopId, cancellationToken);
        if (shopInfo is null)
            throw new NotFoundException(ErrorMessages.Shop_NotFound);

        customer = new Customer
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ShopId = shopId
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync(cancellationToken);
        return customer;
    }
    
    public async Task<Customer> GetCustomer(string name,string userPhoneNumber, int shopId, CancellationToken cancellationToken)
    {
        var user = await userService.AddUser(name, userPhoneNumber, cancellationToken);
        
        var customer = await context.Customers
            .Where(x => x.UserId == user.Id && x.ShopId == shopId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(customer != null)
            return customer;

        var shopInfo = await context.Shops.FirstOrDefaultAsync(x => x.Id == shopId, cancellationToken);
        if (shopInfo is null)
            throw new NotFoundException(ErrorMessages.Shop_NotFound);

        customer = new Customer
        {
            UserId = user.Id,
            ShopId = shopId
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync(cancellationToken);
        return customer;
    }
}