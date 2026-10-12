using Application.Commands.AdminPanel.POS.CreateOwnership;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.POS;

[Route("api/admin-panel/pos-device")]
public class PosDeviceController(IMediator prophet) : PosApiBase
{
    
    [HttpPost("validate")]
    public async Task<CreatePosOwnershipResult> CreatePosOwnership(CreatePosOwnershipCommand request) =>
        await prophet.Send(request);
}