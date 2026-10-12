using Application.Commands.AdminPanel.MenuItems.Create;
using Application.Commands.AdminPanel.MenuItems.Delete;
using Application.Commands.AdminPanel.MenuItems.Edit;
using Application.Commands.AdminPanel.MenuItems.SetStatus;
using Application.Queries.AdminPanel.MenuItems.AdvancedInfo;
using Application.Queries.AdminPanel.MenuItems.Get;
using Application.Queries.AdminPanel.MenuItems.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class MenuItemController(IMediator prophet) : AdminApiBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task Create([FromForm] CreateMenuItemCommand request)
    {
        await prophet.Send(request);

        Created();
    }

    [HttpPut("{id:int}")]
    [Consumes("multipart/form-data")]
    public async Task Edit([FromRoute] int id, [FromForm] EditMenuItemCommand request)
    {
        request.SetId(id);
        await prophet.Send(request);

        Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var request = new DeleteMenuItemCommand(id);
        await prophet.Send(request);

        NoContent();
    }

    [HttpGet]
    public async Task<List<GetAllMenuItemsQueryResult>> GetAll([FromQuery] GetAllMenuItemsQuery request)
    {
        return await prophet.Send(request);
    }

    [HttpGet("{id:int}")]
    public async Task<GetMenuItemsQueryResult> Get(int id)
    {
        var request = new GetMenuItemsQuery(id);
        return await prophet.Send(request);
    }

    [HttpPatch("{id:int}/status")]
    public async Task SetStatus(int id)
    {
        var request = new SetMenuItemStatusCommand
        {
            MenuItemId = id
        };
        await prophet.Send(request);
    }

    [HttpGet("{id:int}/advanced-info")]
    public async Task<GetMenuItemAdvancedInfoResult> GetAdvancedInfo([FromRoute] int id) => await prophet.Send(
        new GetMenuItemAdvancedInfoQuery(id));
}