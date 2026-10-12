using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Pos.Auth.LogOut;

namespace Api.Controllers.POS;

public class AuthController(IMediator prophet) : PosApiBase
{
    [HttpPost("logout")]
    public async Task LogOut(LogOutCommand request) => await prophet.Send(request);
}