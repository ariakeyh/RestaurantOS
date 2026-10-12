using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.MenuItems.Edit;

public class EditMenuItemCommandHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService)
    : IRequestHandler<EditMenuItemCommand>
{
    public async Task Handle(EditMenuItemCommand request, CancellationToken cancellationToken)
    {
        var requestNameTrimmed = request.Name.Trim();
        
        var isDuplicate = await context.MenuItems
            .AnyAsync(x => x.Id != request.Id &&
                           x.CategoryId == request.CategoryId &&
                           x.Name == requestNameTrimmed,
                cancellationToken);

        if (isDuplicate)
            throw new DuplicateException(ErrorMessages.MenuItem_DuplicateByName);

        var shopId = authService.GetShopId();
        var menuItem = await context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.Category.ShopId == shopId, cancellationToken);

        if (menuItem is null)
            throw new KeyNotFoundException(ErrorMessages.MenuItem_NotFound);

        menuItem.UpdatePrice(request.Price, request.Discount);
            
        menuItem.Name = requestNameTrimmed;
        menuItem.Description = request.Description;
        menuItem.Ingredients = request.Ingredients;
        menuItem.CategoryId = request.CategoryId;

        if (request.ImageFile is { Length: > 0 })
        { 
            var imageName = await objectStorage.UploadFile(request.ImageFile, ObjectStorageTypes.MenuItem);
            menuItem.ImageName = imageName;
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}