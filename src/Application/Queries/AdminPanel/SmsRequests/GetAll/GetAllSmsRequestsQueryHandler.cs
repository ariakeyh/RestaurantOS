using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using Domain.Entities.Sms;
using MediatR;

namespace Application.Queries.AdminPanel.SmsRequests.GetAll;

public class GetAllSmsRequestsQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetAllSmsRequestsQuery, Pagination<SmsRequestDto>>
{
    public async Task<Pagination<SmsRequestDto>> Handle(GetAllSmsRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var query = context.SmsRequests.Where(x => x.ShopId == currentShopId).AsQueryable();

        query = ApplyFilters(request, query);

        var result = await query
            .Select(x => new SmsRequestDto(
                x.Id,
                x.Status,
                x.Type,
                x.SellPrice,
                x.TotalDelivered,
                x.TotalFailed,
                x.TotalSent,
                x.ReceptorsCount))
            .ToPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }

    private static IQueryable<SmsRequest> ApplyFilters(GetAllSmsRequestsQuery request, IQueryable<SmsRequest> query)
    {
        if (request.From.HasValue)
            query = query.Where(x => x.CreatedAt >= request.From.Value);
        if (request.To.HasValue)
            query = query.Where(x => x.CreatedAt <= request.To.Value);
        if (request.Type.HasValue)
            query = query.Where(x => x.Type == request.Type);
        return query;
    }
}