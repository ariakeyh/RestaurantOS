using System.Globalization;
using Application.Interface;
using Application.Utils;
using MediatR;

namespace Application.Commands.Customer.Auth.SendOtp;

public class SendOtpCommandHandler(IOtpService otpService,ICacheService cacheService) : IRequestHandler<SendOtpCommand, SendOtpResult>
{
    public async Task<SendOtpResult> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var phoneNumber = request.PhoneNumber.StandardPhoneNumberFormat();
        var code = new Random().Next(10000, 100000).ToString();

        await otpService.SendOtp(phoneNumber, code);
        await cacheService.SetValue(phoneNumber, code);

        return new SendOtpResult(DateTime.Now.AddMinutes(2).ToString(CultureInfo.CurrentCulture));
    }
}