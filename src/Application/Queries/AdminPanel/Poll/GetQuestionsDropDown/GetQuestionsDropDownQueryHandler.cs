using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Poll.GetQuestionsDropDown;

public class GetQuestionsDropDownQueryHandler(IAppDbContext context) : IRequestHandler<GetQuestionsDropDownQuery, List<GetQuestionsDropDownResult>>
{
    public async Task<List<GetQuestionsDropDownResult>> Handle(GetQuestionsDropDownQuery request, CancellationToken cancellationToken)
    {
        return await context.Questions
            .Select(x => new GetQuestionsDropDownResult(x.Id, x.Title, x.Type))
            .ToListAsync(cancellationToken);
    }
}