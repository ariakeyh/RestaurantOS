using Common;
using FluentValidation;

namespace Application.Commands.AdminPanel.MenuItems.Create;

public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.MenuItem_Name_Required);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage(ErrorMessages.MenuItem_CategoryId_Invalid);

        RuleFor(x => x.Price)
            .GreaterThan(1_000)
            .LessThan(10_000_000)
            .WithMessage(ErrorMessages.MenuItem_PriceRange_Invalid);

        RuleFor(x => x.Discount)
            .InclusiveBetween((byte)0, (byte)100)
            .WithMessage(ErrorMessages.MenuItem_DiscountRange_Invalid);
    }
}