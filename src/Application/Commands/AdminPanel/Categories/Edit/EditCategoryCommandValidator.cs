using Common;
using FluentValidation;

namespace Application.Commands.AdminPanel.Categories.Edit;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.Category_Name_Required);
    }
}