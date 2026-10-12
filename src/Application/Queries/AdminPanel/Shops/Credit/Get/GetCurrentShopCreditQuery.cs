using MediatR;

namespace Application.Queries.AdminPanel.Shops.Credit.Get;

public sealed record GetCurrentShopCreditQuery : IRequest<GetCurrentShopCreditResult>;

public sealed record GetCurrentShopCreditResult(int Credit);

    
