using Application.Interface;
using Application.Utils;
using Common;
using Common.Exceptions;
using Domain.Entities.Auth;
using Domain.Entities.Shops;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CustomerPanel.Auth.CheckOtp;

public class CheckOtpCommandHandler(IAppDbContext context, IAuthService authService, ICacheService cacheService)
    : IRequestHandler<CheckOtpCommand, CheckOtpResult>
{
    public async Task<CheckOtpResult> Handle(CheckOtpCommand request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        var phoneNumber = request.PhoneNumber.StandardPhoneNumberFormat();
        await CheckOtp(phoneNumber, request.Code);

        var user = await GetOrCreateCustomer(currentShopId,phoneNumber, cancellationToken);

        return GenerateToken(user);
    }

    private async Task<Domain.Entities.Customers.Customer> GetOrCreateCustomer(int shopId, string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await context
            .Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.User.PhoneNumber == phoneNumber && x.ShopId == shopId, cancellationToken);

        if (user is not null) return user;

        var newCustomer = new Domain.Entities.Customers.Customer
        {
            ShopId = shopId,
            Name = string.Empty,
            Type = ShopUserType.Customer,
            Badge = CustomerBadge.Unknown,
            User = new User
            {
                PhoneNumber = phoneNumber,
                Name = string.Empty
            }
        };

        context.Customers.Add(newCustomer);
        await context.SaveChangesAsync(cancellationToken);
        return newCustomer;
    }

    private async Task CheckOtp(string phoneNumber, string code)
    {
        var otp = await cacheService.GetValue<string>(phoneNumber);

        if (string.IsNullOrEmpty(otp) || otp != code)
            throw new NotFoundException(ErrorMessages.NotFound);
    }

    private CheckOtpResult GenerateToken(ShopUser user)
    {
        var token = authService.Login(user);
        return new CheckOtpResult(token);
    }
}