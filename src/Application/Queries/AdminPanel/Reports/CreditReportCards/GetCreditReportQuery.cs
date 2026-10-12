using Application.Queries.AdminPanel.Reports.ReportsDto;
using MediatR;

namespace Application.Queries.AdminPanel.Reports.CreditReportCards;

public sealed record GetCreditReportQuery(DateTime? From, DateTime? To) : SimpleTextReportingRequest;

public sealed record GetCashBackReportCardsResult(int TotalGivenCredits, int TotalSpentCreditFromGivenCredit, int TotalSpentCredits, int TotalExpiredCredits, int TotalOrdersMadeWithCredit, int TotalOrdersMadeWithCreditPrice);