using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Sms.RequestInfo;

public class GetSmsRequestInfoHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetSmsRequestInfoQuery, GetSmsRequestInfoResult>
{
    public async Task<GetSmsRequestInfoResult> Handle(GetSmsRequestInfoQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        var result = await context.SmsRequests
            .Where(x => x.Id == request.Id && x.ShopId == currentShopId)
            .Select(x => new GetSmsRequestInfoResult(
                x.Id,
                x.Status,
                x.Type,
                x.SellPrice,
                x.TotalDelivered,
                x.TotalFailed,
                x.TotalSent,
                x.ReceptorsCount
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound);
        }

        return result;
    }

}