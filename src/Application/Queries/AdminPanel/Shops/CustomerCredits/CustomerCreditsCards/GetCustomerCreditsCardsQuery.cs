using MediatR;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsCards;

public record GetCustomerCreditsCardsQuery(DateTime? From, DateTime? To) : IRequest<GetCustomerCreditsCardsResult>;

public record GetCustomerCreditsCardsResult(int TotalGivenCredit, int TotalSpentCredit, int TotalExpiredCredit);