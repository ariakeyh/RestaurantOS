using Application.DTOs.SmsIr;
using Application.Interface;
using Domain.Enums;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;

namespace Infra.Sms;

public class SmsIRService(SmsIr smsProvider) : ISmsService
{
    public async Task SendByTemplate(
        string phoneNumber,
        int templateId,
        Dictionary<string, string> parameters)
    {
        var verifySendParameters = parameters
            .Select(p => new VerifySendParameter(p.Key, p.Value))
            .ToArray();

        await smsProvider.VerifySendAsync(phoneNumber, templateId, verifySendParameters);
    }

    public async Task<decimal> GetProviderCredit()
    {
        var response = await smsProvider.GetCreditAsync();
        return response.Data;
    }

    public async Task<SmsSendResult> Send(
        long lineNumber,
        string message,
        string[] numbers,
        int? sendDateTimeInUnix = null)
    {
        var response = await smsProvider.BulkSendAsync(
            lineNumber,
            message,
            numbers,
            sendDateTimeInUnix);

        return MapSendResult(response);
    }

    public async Task<SmsSendResult> Send(
        long lineNumber,
        string[] messages,
        string[] numbers,
        int? sendDateTimeInUnix = null)
    {
        var response = await smsProvider.LikeToLikeSendAsync(
            lineNumber,
            messages,
            numbers,
            sendDateTimeInUnix);

        return MapSendResult(response);
    }

    public async Task<IReadOnlyList<SmsDeliveryStatusResult>> GetPackStatus(string packId)
    {
        var response = await smsProvider.GetReportAsync(new Guid(packId));

        return response.Data.Select(m => new SmsDeliveryStatusResult
        {
            ProviderMessageId = m.MessageId,
            Status = MapDeliveryStatus((int)m.DeliveryState!),
            ProviderFinalDeliveryState = m.DeliveryState!.ToString(),
        }).ToList();
    }

    public async Task<SmsDeliveryStatusResult> GetStatus(int providerMessageId)
    {
        var response = await smsProvider.GetReportAsync(providerMessageId);

        return new SmsDeliveryStatusResult
        {
            ProviderMessageId = providerMessageId,
            Status = MapDeliveryStatus((int)response.Data.DeliveryState!),
            ProviderFinalDeliveryState = response.Data.DeliveryState!.ToString(),
            PhoneNumber = response.Data.Mobile.ToString()
        };
    }
    
    // ----------------- helpers -----------------

    private static SmsSendResult MapSendResult(dynamic response)
    {
        return new SmsSendResult
        {
            PackId = response.Data.PackId,
            MessageIds = response.Data.MessageIds,
            FinalProviderPrice = Convert.ToInt32(
                Math.Round(response.Data.Cost))
        };
    }

    private static SmsReceptorStatus MapDeliveryStatus(int providerStatus)
    {
        return providerStatus switch
        {
            1 => SmsReceptorStatus.Delivered,
            2 => SmsReceptorStatus.Undelivered,
            _ => SmsReceptorStatus.Pending
        };
    }
}