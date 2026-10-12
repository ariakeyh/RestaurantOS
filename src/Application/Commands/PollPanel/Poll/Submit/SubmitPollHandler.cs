using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Polls;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.PollPanel.Poll.Submit;

public class SubmitPollHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<SubmitPollCommand>
{
    public async Task Handle(SubmitPollCommand request, CancellationToken cancellationToken)
    {
        var pollToken = await GetPollToken(request, cancellationToken);

        LoginPollCustomer(pollToken);

        var order = pollToken.Order;

        ValidateFoodRatingsMenuItemsWithOrder(request.FoodRatings, pollToken);

        var pollResponse = CreatePollResponse(request, order);

        context.PollResponses.Add(pollResponse);
        context.PollTokens.Remove(pollToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static PollResponse CreatePollResponse(SubmitPollCommand request, Domain.Entities.Orders.Order order)
    {
        var pollResponse = new PollResponse
        {
            OrderId = order.Id,
            StarRate = request.Rating,
            Note = request.Note,
            QuestionAnswers = new List<PollQuestionAnswer>(),
            FoodRatings = new List<PollFoodRating>()
        };

        foreach (var fr in request.FoodRatings ?? [])
        {
            pollResponse.FoodRatings.Add(new PollFoodRating
            {
                MenuItemId = fr.MenuItemId,
                Rating = fr.Rating,
                Note = fr.Note
            });
        }

        foreach (var q in request.Questions ?? [])
        {
            pollResponse.QuestionAnswers.Add(new PollQuestionAnswer
            {
                QuestionId = q.QuestionId,
                Answer = q.Answer
            });
        }

        return pollResponse;
    }

    private static void ValidateFoodRatingsMenuItemsWithOrder(List<FoodRatingDto> foodRatings, PollToken pollToken)
    {
        var invalidIds = foodRatings
            .Where(fr => !pollToken.Order.Items.Select(x=>x.MenuItemId).Contains(fr.MenuItemId))
            .Select(fr => fr.MenuItemId).ToList();

        if (invalidIds.Count != 0)
            throw new BadRequestException(ErrorMessages.Order_Items_NotFound);
    }

    private void LoginPollCustomer(PollToken? pollToken)
    {
        var shopUser = pollToken.Order.Customer;

        authService.Login(shopUser);
    }

    private async Task<PollToken> GetPollToken(SubmitPollCommand request, CancellationToken cancellationToken)
    {
        var pollToken = await context.PollTokens
            .Include(pt => pt.Order)
            .ThenInclude(x=>x.Customer)
            .ThenInclude(x=>x.User)
            .Include(pt => pt.Order)
            .ThenInclude(x=>x.Items)
            .FirstOrDefaultAsync(pt => pt.Token == request.PollToken, cancellationToken);

        return pollToken ?? throw new NotFoundException(ErrorMessages.Poll_Response_NotFound);
    }
}