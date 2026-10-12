using Domain.Entities.Agg;
using Domain.Enums;

namespace Domain.Entities.Sms;

public class SmsRequestReceptorStatusHistory : AggregateRootWithKey<int>
{
    public int SmsRequestReceptorId { get; set; }
    public SmsRequestReceptor SmsRequestReceptor { get; set; } = null!;

    public SmsReceptorStatus PrevStatus { get; set; }

    public string PrevStatusComment { get; set; }

    public SmsRequestReceptorStatusHistory(SmsReceptorStatus prevStatus, string prevStatusComment)
    {
        PrevStatus = prevStatus;
        PrevStatusComment = prevStatusComment;
    }
}