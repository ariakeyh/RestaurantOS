using Application.Interface;
using Application.Utils;
using Common.Interfaces;
using MediatR;

namespace Application.Queries.AdminPanel.Poll.GetResponses;

public class GetAllPollResponsesQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetAllPollResponsesQuery, Pagination<GetAllPollResponsesResult>>
{
    public async Task<Pagination<GetAllPollResponsesResult>> Handle(GetAllPollResponsesQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var query = context.PollResponses.Where(x=>x.Order.ShopId == currentShopId)
            .OrderByDescending(x=>x.Id)
            .AsQueryable();
       
        
        if (request.Ratings is { Count: > 0 and <= 5 }
            && request.Ratings.All(r => r is >= 1 and <= 5))
        {
            query = query.Where(p => request.Ratings.Contains(p.StarRate));
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(x=>x.CreatedAt >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= request.ToDate.Value);
        }

        var result = await query.Select(x => new GetAllPollResponsesResult
        {
            OrderId = x.OrderId,
            Note = x.Note,
            StarRate = x.StarRate,
            NumberOfFoodRatings = x.FoodRatings.Count,
            NumberOfQuestionsAnswered = x.QuestionAnswers.Count,
            CreateDate = x.CreatedAt.ToPersianDate()
        }).ToPaginationAsync(request.PageNumber,request.PageSize,cancellationToken);

        return result;
    }
}