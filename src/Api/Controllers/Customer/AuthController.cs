using Application.Commands.Customer.Auth.SendOtp;
using Application.Commands.CustomerPanel.Auth.CheckOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Customer;

public class AuthController(IMediator mediator) : CustomerApiBase
{
    [HttpPost]
    public async Task<SendOtpResult> SendOtp(SendOtpCommand request) => 
        await mediator.Send(request);
    
        
    [HttpPost]
    public async Task<CheckOtpResult> CheckOtp(CheckOtpCommand request) => 
        await mediator.Send(request);
}