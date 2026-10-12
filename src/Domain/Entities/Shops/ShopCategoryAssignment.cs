using Domain.Entities.Agg;
using Domain.Entities.Settings;

namespace Domain.Entities.Shops;

public class ShopCategoryAssignment : AggregateRoot
{
    public int ShopId { get; set; }
    public int CategoryId { get; set; }

    public Shop Shop { get; set; }
    public ShopCategory Category { get; set; }
}