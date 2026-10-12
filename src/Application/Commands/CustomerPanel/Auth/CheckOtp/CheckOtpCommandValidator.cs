using Application.Utils;
using Common;
using FluentValidation;

namespace Application.Commands.CustomerPanel.Auth.CheckOtp;

public class CheckOtpCommandValidator : AbstractValidator<CheckOtpCommand>
{
    public CheckOtpCommandValidator()
    {
        RuleFor(x=>x.PhoneNumber).Must(StringHelpers.ValidateMobile).WithMessage(ErrorMessages.PhoneNumber_Invalid);
        RuleFor(x => x.Code).NotEmpty();
    }
}