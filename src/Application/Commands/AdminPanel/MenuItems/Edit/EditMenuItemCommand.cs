using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.AdminPanel.MenuItems.Edit;

public class EditMenuItemCommand : IRequest
{
    internal int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Price { get; set; }
    public byte Discount { get; set; }
    public int CategoryId { get; set; }
    
    public void SetId(int id) => Id = id;
}