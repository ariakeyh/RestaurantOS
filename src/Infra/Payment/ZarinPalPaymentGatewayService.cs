using System.Text;
using Application.Interface;
using Common;
using Common.Exceptions;
using Infra.Payment.CreatePayment;
using Infra.Payment.VerifyPayment;
using Newtonsoft.Json;

namespace Infra.Payment;

public class ZarinPalPaymentGatewayService(
    IHttpClientFactory clientFactory,
    ZarinpalConfig config) : IPaymentGatewayService
{
    private HttpClient Client => clientFactory.CreateClient("ZarinpalClient");

    private string RequestUrl => config.IsSandbox
        ? "https://sandbox.zarinpal.com/pg/v4/payment/request.json"
        : "https://payment.zarinpal.com/pg/v4/payment/request.json";

    private string VerifyUrl => config.IsSandbox
        ? "https://sandbox.zarinpal.com/pg/v4/payment/verify.json"
        : "https://payment.zarinpal.com/pg/v4/payment/verify.json";

    private string StartPayUrl(string authority) => config.IsSandbox
        ? $"https://sandbox.zarinpal.com/pg/StartPay/{authority}"
        : $"https://payment.zarinpal.com/pg/StartPay/{authority}";

    public async Task<(string Authority, string URL)> CreatePaymentLink(
        int amount,
        string description,
        string userPhoneNumber,
        CancellationToken cancellationToken)
    {
        var requestData = new CreatePaymentZarinPalRequest
        {
            MerchantID = config.ApiKey,
            Amount = amount,
            CallbackURL = config.CallbackUrl,
            Mobile = userPhoneNumber,
            Description = description
        };

        var response = await PostAsync(RequestUrl, requestData, cancellationToken);
        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        var parsed = JsonConvert.DeserializeObject<ZarinpalHttpWrapper<CreatePaymentZarinPalResponse>>(result)
                     ?? throw new BadRequestException(ErrorMessages.PG_Error);

        if (parsed.Data.Authority is null)
            throw new BadRequestException(ErrorMessages.PG_Error);

        return (parsed.Data.Authority, StartPayUrl(parsed.Data.Authority));

    }
    

    public async Task<(bool IsSuccess, string Message, string Payload)> VerifyPayment(
        string authority,
        decimal amount,
        CancellationToken cancellationToken)
    {
        var requestData = new VerifyPaymentZarinPalRequest
        {
            MerchantID = config.ApiKey,
            Authority = authority,
            Amount = amount
        };

        var response = await PostAsync(VerifyUrl, requestData, cancellationToken);
        var payload = await response.Content.ReadAsStringAsync(cancellationToken);

        var parsed = JsonConvert.DeserializeObject<ZarinpalHttpWrapper<VerifyPaymentZarinPalResponse>>(payload)
                     ?? throw new BadRequestException(ErrorMessages.PG_Error);

        return (parsed.Data.IsSuccess, parsed.Data.Message, payload);
    }

    private async Task<HttpResponseMessage> PostAsync(string url, object payload, CancellationToken cancellationToken)
    {
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, content, cancellationToken);

        if (!response.IsSuccessStatusCode || response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new BadRequestException(ErrorMessages.PG_Error);

        return response;
    }
}
