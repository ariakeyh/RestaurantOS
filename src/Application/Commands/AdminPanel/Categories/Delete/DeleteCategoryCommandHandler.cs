using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;

namespace Application.Commands.AdminPanel.Categories.Delete;

public class DeleteCategoryCommandHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<DeleteCategoryCommand,DeleteCategoryResult>
{
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var category = await context.Categories.FindAsync([request.Id], cancellationToken);
        
        if (category is null || category.ShopId != shopId)
            throw new NotFoundException(ErrorMessages.Category_NotFound);
        
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteCategoryResult();
    }
}