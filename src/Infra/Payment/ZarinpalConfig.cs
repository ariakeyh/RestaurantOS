namespace Infra.Payment;

public class ZarinpalConfig
{
    public string ApiKey { get; set; } = null!;
    public string CallbackUrl { get; set; } = null!;
    public bool IsSandbox { get; set; }
}