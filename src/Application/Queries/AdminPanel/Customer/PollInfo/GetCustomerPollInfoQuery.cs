using MediatR;

namespace Application.Queries.AdminPanel.Customer.PollInfo;

public record GetCustomerPollInfoQuery (Guid CustomerId) : IRequest<PollInfo>;

public sealed record PollInfo(
    int PollCount,
    double AverageRating,
    Dictionary<DateTime, byte> TopOrderRatings,
    Dictionary<DateTime, byte> BottomOrderRatings,
    Dictionary<string, double> TopMenuItemAverageRatings,
    Dictionary<string, double> BottomMenuItemAverageRatings,
    List<string> AllPollNotes);