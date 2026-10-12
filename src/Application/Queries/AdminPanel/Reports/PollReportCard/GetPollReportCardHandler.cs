using Application.Interface;
using Application.Queries.AdminPanel.Reports.ReportsDto;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Reports.PollReportCard;

public class GetPollReportCardHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetPollReportCardQuery,List<SimpleTextReportingResult>>
{
    public async Task<List<SimpleTextReportingResult>> Handle(GetPollReportCardQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var pollResponses = await context.PollResponses.Where(x => x.Order.ShopId == currentShopId)
            .ToListAsync(cancellationToken);

        return
        [
            new SimpleTextReportingResult(ChartData.Label_TotalPollCount, pollResponses.Count, ChartData.Icon_Poll),
            new SimpleTextReportingResult(ChartData.Label_AveragePollRating, pollResponses.Average(a=>a.StarRate), ChartData.Icon_Star),
            new SimpleTextReportingResult(ChartData.Label_TotalQuestionsAnswered,pollResponses.Select(x=>x.QuestionAnswers).Count(),ChartData.Icon_Question)
        ];
    }
}