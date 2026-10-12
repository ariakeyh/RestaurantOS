using Application.Commands.AdminPanel.Poll.ChangeSetting;
using Application.Queries.AdminPanel.Poll.GetQuestionsDropDown;
using Application.Queries.AdminPanel.Poll.GetResponseDetails;
using Application.Queries.AdminPanel.Poll.GetResponses;
using Application.Queries.AdminPanel.Poll.ShopPoll;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class PollController(IMediator prophet) : AdminApiBase
{
    [HttpGet]
    public async Task<GetShopPollQueryResult?> GetPoll() => 
        await prophet.Send(new GetShopPollQuery());
    
    [HttpGet("responses")]
    public async Task<Pagination<GetAllPollResponsesResult>> GetAllPollResponses([FromQuery] GetAllPollResponsesQuery query) =>
        await prophet.Send(query);

    [HttpGet("responses/{id}")]
    public async Task<GetPollResponseDetailsResult> GetPollResponseDetails([FromRoute] int id)
    {
        var request = new GetPollResponseDetailsQuery();
        request.SetId(id);
        return await prophet.Send(request);
    }
    
    [HttpGet("questions/drop-down")]
    public async Task<List<GetQuestionsDropDownResult>> GetQuestionsDropDown([FromQuery] GetQuestionsDropDownQuery query) => 
        await prophet.Send(query);

    [HttpPost]
    public async Task<ChangePollSettingResult> ChangePollSetting([FromBody] ChangePollSettingCommand request) =>
        await prophet.Send(request);

}