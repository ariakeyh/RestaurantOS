using Common.Interfaces;
using Domain.Enums;

namespace Application.Queries.AdminPanel.Shops.Credit.CreditHistory.GetAll;

public sealed record GetAllCreditHistoryQuery(CreditHistoryType? Type,DateTime? From, DateTime? To) : PaginationRequest<CreditHistoryDto>;
public sealed record CreditHistoryDto(int Id,int Amount,CreditHistoryType Type, int? SmsRequestId, DateTime CreatedAt);