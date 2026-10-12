using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Poll.ShopPoll;

public class GetShopPollQueryHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<GetShopPollQuery, GetShopPollQueryResult?>
{
    public async Task<GetShopPollQueryResult?> Handle(GetShopPollQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        
        var poll = await context.Polls.Where(x=>x.ShopId == currentShopId)
            .Select(x=> new GetShopPollQueryResult(x.ShouldRateFoods,x.NotifyAfterDays, x.Questions.Select(q=>q.QuestionId).ToList()))
            .FirstOrDefaultAsync(cancellationToken);
        
        return poll;
    }
}