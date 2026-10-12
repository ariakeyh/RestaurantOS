namespace Application.DTOs.SmsIr;

public sealed class SmsSendResult
{
    /// <summary>
    /// sms.ir packId – batch identifier
    /// </summary>
    public string PackId { get; init; } = default!;


    /// <summary>
    /// Provider message IDs in the SAME ORDER as input numbers
    /// </summary>
    public IReadOnlyList<int> MessageIds { get; init; } = [];


    /// <summary>
    /// Provider cost converted to int 
    /// </summary>
    public int FinalProviderPrice { get; init; }
}