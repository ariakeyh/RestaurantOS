using Application.Utils;
using Common;
using FluentValidation;

namespace Application.Commands.Customer.Auth.SendOtp;

public class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
{
    public SendOtpCommandValidator()
    {
        RuleFor(x=>x.PhoneNumber).Must(StringHelpers.ValidateMobile).WithMessage(ErrorMessages.PhoneNumber_Invalid);
    }
}