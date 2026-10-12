using FluentValidation;

namespace Application.Commands.AdminPanel.Categories.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}