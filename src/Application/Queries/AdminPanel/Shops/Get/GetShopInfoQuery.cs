using MediatR;

namespace Application.Queries.AdminPanel.Shops.Get;

public class GetShopQuery : IRequest<GetShopQueryResult>;

public record GetShopQueryResult(
    string Name,
    string Description,
    string Address,
    string ContactInfo,
    string ImageName,
    byte NumberOfTables
);
