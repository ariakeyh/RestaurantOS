using Domain.Entities.Agg;
using Domain.Entities.Shops;

namespace Domain.Entities.Settings;

public class ShopCategory : AggregateRootWithKey<int>
{
    public string Title { get; set; }

    public ICollection<ShopCategoryAssignment> Shops { get; set; }

}