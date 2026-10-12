using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Poll.GetQuestionsDropDown;

public sealed record GetQuestionsDropDownQuery : IRequest<List<GetQuestionsDropDownResult>>;

public sealed record GetQuestionsDropDownResult(int Id, string Title, QuestionType Type);