using Newtonsoft.Json;

namespace Infra.Payment.VerifyPayment;


public class VerifyPaymentZarinPalRequest
{
    [JsonProperty("merchant_id")]
    public string MerchantID { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    
    [JsonProperty("authority")]
    public string Authority { get; set; }
}