using Domain.Enums;

namespace Application.DTOs.SmsIr;

public sealed class SmsDeliveryStatusResult
{
    public int ProviderMessageId { get; init; }
    public SmsReceptorStatus Status { get; init; }
    public string? ProviderFinalDeliveryState { get; set; } = string.Empty;
    public string? PhoneNumber  { get; set; }
}