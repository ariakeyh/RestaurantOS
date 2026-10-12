using Application.Queries.MenuShowcase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.POS;

public class MenuController(IMediator mediator) : PosApiBase
{
    [HttpGet]
    public async Task<List<MenuShowcaseQueryResult>> Showcase()
    {
        return await mediator.Send(new MenuShowcaseQuery());
    }
}