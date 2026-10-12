using Application.Commands.AdminPanel.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class AuthController(IMediator prophet) : AdminApiBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<AdminLoginResult> Login(AdminLoginRequest request) => 
        await prophet.Send(request);
}