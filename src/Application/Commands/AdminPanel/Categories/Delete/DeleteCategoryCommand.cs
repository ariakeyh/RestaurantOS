using MediatR;

namespace Application.Commands.AdminPanel.Categories.Delete;

public record DeleteCategoryCommand (int Id) : IRequest<DeleteCategoryResult>;

public record DeleteCategoryResult;