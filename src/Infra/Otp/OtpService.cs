using Application.Interface;
using Infra.Sms;

namespace Infra.Otp;

public class OtpService(ISmsService smsService,SmsProviderConfig smsProviderConfig) : IOtpService
{
    public async Task<bool> SendOtp(string phoneNumber, string code)
    {
        var verifySendParameters = new Dictionary<string, string>
        {
            { nameof(code) + " : ", code }
        };

        await smsService.SendByTemplate(phoneNumber,smsProviderConfig.OtpSmsTemplateId , verifySendParameters);

        return true;
    }
}