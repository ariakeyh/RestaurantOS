using Application.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Infra.Captcha;

public class CaptchaService(CaptchaConfig captchaConfig, IHttpClientFactory httpClientFactory, ILogger<CaptchaService> logger) : ICaptchaService
{
    public async Task<bool> VerifyAsync(string responseToken,
        CancellationToken cancellationToken = default)
    {
        using var client = httpClientFactory.CreateClient(nameof(CaptchaService));
        var response = await client.GetStringAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={captchaConfig.SecretKey}&response={responseToken}",
            cancellationToken);
        if (string.IsNullOrWhiteSpace(response))
            return false;

        try
        {
            var jsonResponse = JObject.Parse(response);

            return (jsonResponse["success"] ?? false).Value<bool>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Captcha validation failed.");
            return false;
        }
    }
}