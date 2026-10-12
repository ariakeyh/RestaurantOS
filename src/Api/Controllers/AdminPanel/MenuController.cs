using Application.Queries.AdminPanel.Menu.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class MenuController(IMediator prophet) : AdminApiBase
{
    [HttpGet]
    public async Task<GetMenuResult> GetMenu() => await prophet.Send(new GetMenuQuery());
}