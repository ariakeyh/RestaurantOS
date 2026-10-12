using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.CreditReportCards;

public class GetCreditReportCardsHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCreditReportQuery, List<SimpleTextReportingResult>>
{
    public async Task<List<SimpleTextReportingResult>> Handle(GetCreditReportQuery request,
        CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        
        var (from, to) = request.GetNormalizedRange();

        var creditsQuery = context.CustomerCredits
            .AsNoTracking()
            .Where(c => c.Customer.ShopId == shopId);

        var appliedCreditsQuery = context.CustomerCreditSpendHistories
            .AsNoTracking()
            .Where(x => x.Credit.Customer.ShopId == shopId);

        if (request.From.HasValue)
        {
            creditsQuery = creditsQuery.Where(x => x.CreatedAt >= from);
            appliedCreditsQuery = appliedCreditsQuery.Where(x => x.CreatedAt >= from);
        }

        if (request.To.HasValue)
        {
            creditsQuery = creditsQuery.Where(x => x.CreatedAt <= to);
            appliedCreditsQuery = appliedCreditsQuery.Where(x => x.CreatedAt <= to);
        }

        var creditsData = await creditsQuery
            .Select(x => new { x.Amount, x.SpentAmount, x.Expiration })
            .ToListAsync(cancellationToken);

        var appliedCreditsData = await appliedCreditsQuery
            .Select(x => new { x.SpendAmount, OrderPrice = x.Order.TotalPriceWithDiscount })
            .ToListAsync(cancellationToken);

        var totalGivenCredit = creditsData.Sum(x => x.Amount);
        var totalSpentCreditFromGivenCredit = creditsData.Sum(x => x.SpentAmount);
        var expiredCreditsCount = creditsData.Count(x =>
            (!request.From.HasValue || x.Expiration >= request.From.Value) &&
            (!request.To.HasValue || x.Expiration <= request.To.Value));

        var totalSpentCredit = appliedCreditsData.Sum(x => x.SpendAmount);
        var totalOrdersMadeWithCredit = appliedCreditsData.Count;
        var totalOrdersMadeWithCreditPrice = appliedCreditsData.Sum(x => x.OrderPrice);

        return
        [
            new SimpleTextReportingResult(ChartData.Label_TotalGivenCredit, totalGivenCredit,
                ChartData.Icon_Credit_Card),
            new SimpleTextReportingResult(ChartData.Label_TotalSpentCreditFromGivenCredit,
                totalSpentCreditFromGivenCredit, ChartData.Icon_Money_Down),
            new SimpleTextReportingResult(ChartData.Label_ExpiredCreditCount, expiredCreditsCount,
                ChartData.Icon_Timer),
            new SimpleTextReportingResult(ChartData.Label_TotalSpentCredit, totalSpentCredit, ChartData.Icon_Money),
            new SimpleTextReportingResult(ChartData.Label_TotalOrdersMadeWithCredit, totalOrdersMadeWithCredit,
                ChartData.Icon_Shoppping_Cart),
            new SimpleTextReportingResult(ChartData.Label_TotalOrdersMadeWithCreditPrice,
                totalOrdersMadeWithCreditPrice, ChartData.Icon_Receipt)
        ];
    }
}