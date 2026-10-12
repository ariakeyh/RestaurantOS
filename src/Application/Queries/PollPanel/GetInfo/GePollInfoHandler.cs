using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.PollPanel.GetInfo;

public class GePollInfoHandler(IAppDbContext context, IObjectStorage objectStorage)
    : IRequestHandler<GetPollInfoQuery, GetPollInfoResult>
{
    public async Task<GetPollInfoResult> Handle(GetPollInfoQuery request, CancellationToken cancellationToken)
    {
        var pollToken = await context.PollTokens
            .AsNoTracking()
            .Where(pt => pt.Token == request.PollToken)
            .Select(pt => new
            {
                pt.Order,
                Items = pt.Order.Items.Select(oi => new
                {
                    oi.MenuItem.Name,
                    oi.MenuItem.ImageName
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (pollToken is null)
            throw new NotFoundException(ErrorMessages.Poll_Response_NotFound);

        var order = pollToken.Order;

        var orderInfo = new OrderInfo(
            TotalPrice: order.TotalPrice,
            TotalPriceWithDiscount: order.TotalPriceWithDiscount,
            Date: order.CreatedAt
        );

        var menuItems = await Task.WhenAll(
            pollToken.Items.Select(async item =>
                new MenuItemsInfo(
                    Name: item.Name,
                    ImageName: await objectStorage.GetFileUrl(item.ImageName)
                )
            )
        );
        
        var poll = await context.Polls
            .AsNoTracking()
            .Where(p => p.ShopId == order.ShopId)
            .Select(p => new
            {
                p.ShouldRateFoods,
                Questions = p.Questions.Select(q => new PollQuestions(
                    q.Id,
                    q.Question.Title,
                    q.Question.Type
                )).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (poll is null)
            throw new NotFoundException(ErrorMessages.PollSettings_NotFound);

        return new GetPollInfoResult(
            OrderInfo: orderInfo,
            MenuItems: menuItems.ToList(),
            ShouldRateFoods: poll.ShouldRateFoods,
            Questions: poll.Questions
        );
    }
}