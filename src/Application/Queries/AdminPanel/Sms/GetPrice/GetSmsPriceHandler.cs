using Application.Services;
using MediatR;

namespace Application.Queries.AdminPanel.Sms.GetPrice;

public class GetSmsPriceHandler(SmsPriceProviderService smsPriceProviderService) : IRequestHandler<GetSmsPriceQuery, GetSmsPriceQueryResult>
{
    public async Task<GetSmsPriceQueryResult> Handle(GetSmsPriceQuery request, CancellationToken cancellationToken)
    {
        var smsPrices = await smsPriceProviderService.CalculatePricing(request.Message.Length, request.PriceType);
        return new GetSmsPriceQueryResult(smsPrices.SellPrice);
    }
}