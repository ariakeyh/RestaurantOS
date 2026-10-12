using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.AdminPanel.SmsRequests.GetAll;

public sealed record GetAllSmsRequestsQuery(DateTime? From, DateTime? To, SmsRequestType? Type)
    : PaginationRequest<SmsRequestDto>;

public sealed record SmsRequestDto(
    int Id,
    SmsRequestStatus Status,
    SmsRequestType Type,
    int SellPrice,
    int TotalDelivered,
    int TotalFailed,
    int TotalSent,
    int ReceptorsCount);