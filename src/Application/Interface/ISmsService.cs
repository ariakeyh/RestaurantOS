using Application.DTOs.SmsIr;

namespace Application.Interface;

public interface ISmsService
{
    public Task<SmsSendResult> Send(long lineNumber, string message, string[] numbers, int? sendDateTimeInUnix = null);
    public Task<SmsSendResult> Send(long lineNumber, string[] messages, string[] numbers, int? sendDateTimeInUnix = null);
    
    public Task SendByTemplate(string phoneNumber, int templateId, Dictionary<string, string> parameters);
    public Task<decimal> GetProviderCredit();
    public Task<IReadOnlyList<SmsDeliveryStatusResult>> GetPackStatus(string providerPackId);
    public Task<SmsDeliveryStatusResult> GetStatus(int providerMessageId);
    
}