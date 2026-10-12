using Newtonsoft.Json;

namespace Infra.Payment;

public class ZarinpalHttpWrapper<T>
{
    [JsonProperty("data")]
    public T Data { get; set; } 
}