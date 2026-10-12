using Common;
using FluentValidation;

namespace Application.Commands.AdminPanel.ShopInfos.Update;

public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
{
    public UpdateShopCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ErrorMessages.Shop_Name_Required);

        RuleFor(x => x.NumberOfTables)
            .GreaterThan((byte)0).WithMessage(ErrorMessages.Shop_NumberOfTables_Required);

    }
}