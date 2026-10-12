using MediatR;

namespace Application.Queries.AdminPanel.Orders.GetMenuItems;

public record GetOrderMenuItems(int Id) : IRequest<List<OrderMenuItemDto>>;
public class OrderMenuItemDto
{
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int PriceWithDiscount { get; set; }
    
    public int TotalPrice => Quantity * Price;
    public int TotalPriceWithDiscount => Quantity * PriceWithDiscount;
}