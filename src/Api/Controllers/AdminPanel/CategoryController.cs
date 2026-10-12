using Application.Commands.AdminPanel.Categories.Create;
using Application.Commands.AdminPanel.Categories.Delete;
using Application.Commands.AdminPanel.Categories.Edit;
using Application.Queries.AdminPanel.Categories.Get;
using Application.Queries.AdminPanel.Categories.GetAll;
using Application.Queries.AdminPanel.Categories.GetDropdown;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminPanel;

public class CategoryController(IMediator prophet) : AdminApiBase
{
    [HttpPost]
    public async Task<CreateCategoryResult> Create(CreateCategoryCommand request)
    { 
        return await prophet.Send(request);
    }

    [HttpPut("{id:int}")]
    public async Task Edit([FromRoute] int id, EditCategoryCommand request)
    {
        request.SetId(id);
        await prophet.Send(request);
        
        Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var request = new DeleteCategoryCommand(id);
        await prophet.Send(request);
        
        NoContent();
    }
    
    [HttpGet]
    public async Task<List<GetAllCategoriesQueryResult>> GetAll([FromQuery] GetAllCategoriesQuery request)
    {
        return await prophet.Send(request);
    }

    [HttpGet("{id:int}")]
    public async Task<GetCategoriesQueryResult> GetAll(int id)
    {
        var request = new GetCategoriesQuery(id);
        return await prophet.Send(request);
    }
    
    [HttpGet("dropdown")]
    public async Task<List<GetDropdownCategoriesQueryResult>> GetDropdown()
    {
        var request = new GetDropdownCategoriesQuery();
        return await prophet.Send(request);
    }
}