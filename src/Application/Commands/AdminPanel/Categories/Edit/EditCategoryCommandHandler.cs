using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Categories.Edit;

public class EditCategoryCommandHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<EditCategoryCommand,EditCategoryResult>
{
    public async Task<EditCategoryResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var requestNameTrimmed = request.Name.Trim();
        if (await context.Categories.AnyAsync(x => x.Id != request.Id && x.Name == requestNameTrimmed,
                cancellationToken))
            throw new DuplicateException(ErrorMessages.Category_DuplicateByTitle);

        var category = await context.Categories.FindAsync([request.Id], cancellationToken);
        if (category is null || category.ShopId != shopId)
            throw new NotFoundException(ErrorMessages.Category_NotFound);

        category.Name = requestNameTrimmed;
        category.ImageName = request.ImageName;

        await context.SaveChangesAsync(cancellationToken);

        return new EditCategoryResult();
    }
}