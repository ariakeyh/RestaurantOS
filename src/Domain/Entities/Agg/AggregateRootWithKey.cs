namespace Domain.Entities.Agg;

public class AggregateRootWithKey<TKey> : AggregateRoot where TKey : struct
{
    public TKey Id { get; set; }
}