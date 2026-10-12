using MediatR;

namespace Application.Commands.AdminPanel.Categories.Create;

public sealed record CreateCategoryResult(int Id);
public class CreateCategoryCommand : IRequest<CreateCategoryResult>
{
    public string Name { get; set; }
    public string ImageName { get; set; }
}