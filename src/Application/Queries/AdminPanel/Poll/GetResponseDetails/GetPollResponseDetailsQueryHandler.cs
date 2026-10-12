using Application.Interface;
using Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsInfo;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Poll.GetResponseDetails;

public class GetPollResponseDetailsQueryHandler(IAppDbContext context,IAuthService authService) : IRequestHandler<GetPollResponseDetailsQuery, GetPollResponseDetailsResult>
{
    public async Task<GetPollResponseDetailsResult> Handle(GetPollResponseDetailsQuery request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        
        var pollResponse = await context.PollResponses
            .AsNoTracking()
            .Where(x=>x.Id == request.Id && x.Order.ShopId == currentShopId)
            .Select(x=> new GetPollResponseDetailsResult(new OrderInfoDto(x.OrderId,x.Order.CreatedAt, x.Order.TotalPrice), 
                new CustomerInfoDto(x.Order.Customer.User.Name, x.Order.Customer.User.PhoneNumber),x.StarRate,x.Note,
                x.FoodRatings.Select(r=> new FoodRatingDto(r.MenuItem.Name,r.MenuItem.ImageName,r.Rating)).ToList(),
                x.QuestionAnswers.Select(q=> new QuestionsAnswerDto(q.Question.Question.Title,q.Question.Question.Type,q.Answer)).ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if (pollResponse is null)
            throw new NotFoundException(ErrorMessages.Poll_Response_NotFound);
        
        return pollResponse;
    }
}