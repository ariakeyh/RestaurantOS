using Newtonsoft.Json;

namespace Infra.Payment.CreatePayment;

public class CreatePaymentZarinPalResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("authority")]
    public string Authority { get; set; }

    [JsonProperty("fee_type")]
    public string FeeType { get; set; }

    [JsonProperty("fee")]
    public int Fee { get; set; }

    public string EndUrl { get; set; }
}