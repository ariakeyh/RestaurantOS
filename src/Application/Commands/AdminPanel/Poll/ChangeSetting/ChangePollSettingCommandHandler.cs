using MediatR;
using Application.Interface;
using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Poll.ChangeSetting;

public class ChangePollSettingHandler(IAppDbContext context, IAuthService authService) 
    : IRequestHandler<ChangePollSettingCommand, ChangePollSettingResult>
{
    public async Task<ChangePollSettingResult> Handle(ChangePollSettingCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var poll = await context.Polls
            .Include(p => p.Questions)
            .ThenInclude(pq => pq.Question)
            .FirstOrDefaultAsync(p => p.ShopId == shopId, cancellationToken);

        if (poll is null)
        {
            poll = CreateNewPoll(request, shopId);
            context.Polls.Add(poll);
        }
        else
        {
            UpdatePoll(request, poll);
        }

        await context.SaveChangesAsync(cancellationToken);

        return new ChangePollSettingResult();
    }

    private static void UpdatePoll(ChangePollSettingCommand request, Domain.Entities.Polls.Poll poll)
    {
        poll.ShouldRateFoods = request.ShouldRateFood;
        poll.NotifyAfterDays = request.NotifyAfterDays;

        SyncPollQuestions(poll, request.QuestionsIds);
    }

    private static Domain.Entities.Polls.Poll CreateNewPoll(ChangePollSettingCommand request, int shopId)
    {
        return new Domain.Entities.Polls.Poll
        {
            ShopId = shopId,
            ShouldRateFoods = request.ShouldRateFood,
            NotifyAfterDays = request.NotifyAfterDays,
            Questions = request.QuestionsIds
                .Select(id => new PollQuestion { QuestionId = id })
                .ToList()
        };
    }

    private static void SyncPollQuestions(Domain.Entities.Polls.Poll poll, int[] questionsIds)
    {
        var requestedIds = new HashSet<int>(questionsIds);
        var existingIds = poll.Questions.Select(q => q.Question.Id).ToHashSet();

        var toRemove = poll.Questions
            .Where(q => !requestedIds.Contains(q.Question.Id))
            .ToList();

        foreach (var pq in toRemove)
        {
            poll.Questions.Remove(pq);
        }

        foreach (var id in requestedIds.Except(existingIds))
        {
            poll.Questions.Add(new PollQuestion { QuestionId = id });
        }
    }

}
