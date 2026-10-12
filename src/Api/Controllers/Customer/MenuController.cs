using Application.Queries.MenuShowcase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Customer;

public class MenuController(IMediator mediator) : CustomerApiBase
{
    [HttpGet]
    public async Task<List<MenuShowcaseQueryResult>> Showcase()
    {
        return await mediator.Send(new MenuShowcaseQuery());
    }
}