using Application.Commands.AdminPanel.POS.CreateToken;
using Application.Queries.AdminPanel.POS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

[Route("api/admin-panel/pos-device")]
public class PosDeviceController(IMediator prophet) : AdminApiBase
{
    [HttpPost("token")]
    public async Task<CreatePosTokenResult> CreatePosToken() =>
        await prophet.Send(new CreatePosTokenCommand());
    
    [HttpGet]
    public async Task<List<PosDeviceDto>> GetPosDevices() => 
        await prophet.Send(new GetPosDevicesCommand());
}