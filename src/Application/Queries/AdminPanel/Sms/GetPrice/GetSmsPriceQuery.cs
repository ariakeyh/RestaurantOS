using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Sms.GetPrice;

public sealed record GetSmsPriceQuery(string Message, SmsPriceType PriceType) : IRequest<GetSmsPriceQueryResult>;
public sealed record GetSmsPriceQueryResult(int SellPrice);