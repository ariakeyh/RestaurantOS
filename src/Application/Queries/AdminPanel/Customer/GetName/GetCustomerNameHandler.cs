using Application.Interface;
using Application.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Customer.GetName;

public class GetCustomerNameHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetCustomerNameQuery, string?>
{
    public async Task<string?> Handle(GetCustomerNameQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var phoneNumber = request.PhoneNumber.StandardPhoneNumberFormat();
        
        return await context.Customers
            .Where(x =>
                x.ShopId == shopId &&
                x.User.PhoneNumber == phoneNumber)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);
    }
}