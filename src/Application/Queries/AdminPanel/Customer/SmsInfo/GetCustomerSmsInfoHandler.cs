using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Customer.SmsInfo;

public class GetCustomerSmsInfoHandler (IAppDbContext context, IAuthService authService) : IRequestHandler<GetCustomerSmsInfoQuery, SmsInfo>
{
    public async Task<SmsInfo> Handle(GetCustomerSmsInfoQuery request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var phoneNumber = await context.Customers.Where(x => x.Id == request.CustomerId && x.ShopId == shopId)
            .Select(x => x.User.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (phoneNumber is null)
            throw new NotFoundException(ErrorMessages.User_NotFound);
        
        var smsReceptors = await context.SmsRequestReceptors.Include(sr=>sr.SmsRequest)
            .Where(sr => sr.ReceptorNumber == phoneNumber)
            .ToListAsync(cancellationToken);

        var sentSmsCount = smsReceptors.Count;
        var smsTypesCount = smsReceptors
            .GroupBy(sr => sr.SmsRequest.Type)
            .ToDictionary(g => g.Key.ToString(), g => g.Count());

        var firstSentSms = smsReceptors.Min(sr => sr.SendDate);
        var lastSentSms = smsReceptors.Max(sr => sr.SendDate);
        var totalSmsCost = smsReceptors.Sum(sr => sr.SellPrice);

        var smsInfo = new SmsInfo(
            SentSmsCount: sentSmsCount,
            SmsTypesCount: smsTypesCount,
            FirstSentSms: firstSentSms,
            LastSentSms: lastSentSms,
            TotalSmsCost: totalSmsCost
        );
        return smsInfo;
    }
}