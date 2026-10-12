using MediatR;

namespace Application.Queries.AdminPanel.Customer.SmsInfo;

public record GetCustomerSmsInfoQuery(Guid CustomerId) : IRequest<SmsInfo>;
public sealed record SmsInfo(
    int SentSmsCount,
    Dictionary<string, int> SmsTypesCount,
    DateTime FirstSentSms,
    DateTime LastSentSms,
    int TotalSmsCost);