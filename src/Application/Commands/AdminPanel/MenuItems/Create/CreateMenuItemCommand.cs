using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.AdminPanel.MenuItems.Create;

public class CreateMenuItemCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Price { get; set; }
    public byte Discount { get; set; }
    public int CategoryId { get; set; }
}