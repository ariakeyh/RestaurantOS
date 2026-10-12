using Domain.Entities.Agg;

namespace Domain.Entities.Settings;

public class Province : AggregateRootWithKey<int>
{
    public string Name { get; set; }

    public ICollection<City> Cities { get; set; }
}