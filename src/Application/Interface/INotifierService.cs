namespace Application.Interface;

public interface INotifierService
{
    Task NotifyEvent(object payload,CancellationToken cancellationToken = default);
    Task NotifyStaleOrders(List<StaleOrderNotification> notifications, CancellationToken cancellationToken = default);
}

public class StaleOrderNotification
{
    public int NumberOfStaleOrders { get; set; }
    public int StaleInMinutes { get; set; }
    public int ShopId { get; set; }
}