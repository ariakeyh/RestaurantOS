using Newtonsoft.Json;

namespace Infra.Payment.VerifyPayment;

public class VerifyPaymentZarinPalResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    public bool IsSuccess => Code == 100;

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("card_hash")]
    public string CardHash { get; set; }

    [JsonProperty("card_pan")]
    public string CardPan { get; set; }

    [JsonProperty("ref_id")]
    public int RefId { get; set; }

    [JsonProperty("fee_type")]
    public string FeeType { get; set; }

    [JsonProperty("fee")]
    public int Fee { get; set; }
}
