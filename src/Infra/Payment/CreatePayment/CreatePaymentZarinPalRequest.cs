using Newtonsoft.Json;

namespace Infra.Payment.CreatePayment;

public class CreatePaymentZarinPalRequest
{
    [JsonProperty("merchant_id")]
    public string MerchantID { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("callback_url")]
    public string CallbackURL { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}