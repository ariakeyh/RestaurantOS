using MediatR;

namespace Application.Commands.AdminPanel.Categories.Edit;

public class EditCategoryCommand : IRequest<EditCategoryResult>
{
    internal int Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    
    public void SetId(int id) => Id = id;
}

public record EditCategoryResult; 