using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.AdminPanel.ShopInfos.Update;

public class UpdateShopCommand : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string ContactInfo { get; set; }
    public IFormFile ImageFile { get; set; }
    public byte NumberOfTables { get; set; }
}