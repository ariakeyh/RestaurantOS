using Domain.Entities.Agg;
using Domain.Entities.Shops;

namespace Domain.Entities.Menu;

public class Category : AggregateRootWithKey<int>
{
    public string Name { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int ShopId { get; set; }

    public Shop Shop { get; set; }
    public ICollection<MenuItem> Items { get; set; }
}