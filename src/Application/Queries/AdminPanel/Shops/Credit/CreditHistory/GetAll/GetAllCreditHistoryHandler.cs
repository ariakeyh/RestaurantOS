using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using MediatR;

namespace Application.Queries.AdminPanel.Shops.Credit.CreditHistory.GetAll;

public class GetAllCreditHistoryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetAllCreditHistoryQuery, Pagination<CreditHistoryDto>>
{
    public async Task<Pagination<CreditHistoryDto>> Handle(GetAllCreditHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var query = context.CreditHistories
            .Where(x => x.ShopId == currentShopId)
            .OrderByDescending(x => x.CreatedAt)
            .AsQueryable();

        query = ApplyFilters(request, query);

        var result = await query.Select(x => new CreditHistoryDto(
                x.Id,
                x.Amount,
                x.Type,
                x.SmsRequest == null ? null : x.SmsRequest.Id,
                x.CreatedAt))
            .ToPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }

    private static IQueryable<Domain.Entities.Shops.CreditHistory> ApplyFilters(GetAllCreditHistoryQuery request,
        IQueryable<Domain.Entities.Shops.CreditHistory> query)
    {
        if (request.Type.HasValue)
            query = query.Where(x => x.Type == request.Type);

        if (request.From.HasValue)
            query = query.Where(x => x.CreatedAt >= request.From.Value);

        if (request.To.HasValue)
            query = query.Where(x => x.CreatedAt <= request.To.Value);

        return query;
    }
}