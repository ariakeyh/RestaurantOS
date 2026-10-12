namespace Application.Services.OrderServices;

public class OrderMarkettingInfoDto
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public string ShopName { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerName { get; set; }
    public int TotalPrice { get; set; }
    public int TotalPriceWithDiscount { get; set; }
}