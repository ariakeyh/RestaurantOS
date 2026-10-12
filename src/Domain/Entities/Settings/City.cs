using Domain.Entities.Agg;

namespace Domain.Entities.Settings;

public class City : AggregateRootWithKey<int>
{
    public int ProvinceId { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Province Province { get; set; }
}