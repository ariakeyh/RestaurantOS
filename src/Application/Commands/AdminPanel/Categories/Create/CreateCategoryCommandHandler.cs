using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Categories.Create;

public class CreateCategoryCommandHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<CreateCategoryCommand,CreateCategoryResult>
{
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var requestNameTrimmed = request.Name.Trim();
        var shopId = authService.GetShopId();
        
        if (await context.Categories.AnyAsync(x => x.Name == requestNameTrimmed, cancellationToken))
            throw new DuplicateException(ErrorMessages.Category_DuplicateByTitle);
        
        var newCategory = new Category
        {
            Name = requestNameTrimmed,
            ImageName = request.ImageName,
            ShopId = shopId
        };

        context.Categories.Add(newCategory);
        await context.SaveChangesAsync(cancellationToken);
        return new CreateCategoryResult(newCategory.Id);
    }
}