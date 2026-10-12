using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Sms.RequestInfo;

public sealed record GetSmsRequestInfoQuery(int Id) : IRequest<GetSmsRequestInfoResult>;

public sealed record GetSmsRequestInfoResult(int Id, SmsRequestStatus Status, SmsRequestType Type, int SellPrice, int TotalDelivered,int TotalFailed, int TotalSent, int ReceptorsCount);