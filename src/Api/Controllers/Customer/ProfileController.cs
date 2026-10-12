using Application.Commands.Customer.Profile;
using Application.Queries.Customer.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Customer;

public class ProfileController(IMediator mediator) : CustomerApiBase
{
    [HttpGet]
    public async Task<CustomerGetProfileResult> GetProfile([FromQuery] CustomerGetProfileCommand request) => 
        await mediator.Send(request);

    [HttpPut("{id:int}")]
    public async Task<CustomerEditProfileResult> EditProfile([FromRoute] int id,[FromForm] CustomerEditProfileCommand request)
    {
        request.SetId(id);
        return await mediator.Send(request);
    }
}