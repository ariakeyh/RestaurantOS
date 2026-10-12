using Application.Commands.PollPanel.Poll.Submit;
using Application.Queries.PollPanel.GetInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PollPanel;

public class PollController(IMediator prophet) : PollApiBase
{
    [HttpGet("info")]
    public async Task<GetPollInfoResult> GetPollInfo([FromQuery] GetPollInfoQuery request) => await prophet.Send(request);
    
    [HttpPost("submit")]
    public async Task SubmitPoll(SubmitPollCommand request) => await prophet.Send(request);
}