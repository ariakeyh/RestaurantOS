using Common;
using FluentValidation;

namespace Application.Commands.AdminPanel.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage(ErrorMessages.Category_Name_Required);
    }
}