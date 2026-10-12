using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.MenuItems.Create;

public class CreateMenuItemCommandHandler(IAppDbContext context, IAuthService authService, IObjectStorage objectStorage) : IRequestHandler<CreateMenuItemCommand>
{
    public async Task Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var requestNameTrimmed = request.Name.Trim();

        await ValidateCategory(request.CategoryId, cancellationToken);

        var isDuplicate = await context.MenuItems
            .AnyAsync(x => x.CategoryId == request.CategoryId
                           && x.Name == requestNameTrimmed,
                cancellationToken);

        if (isDuplicate)
            throw new DuplicateException(ErrorMessages.MenuItem_DuplicateByName);

        var menuItem = new MenuItem
        {
            Name = requestNameTrimmed,
            Description = request.Description,
            Ingredients = request.Ingredients,
            Price = request.Price,
            Discount = request.Discount,
            CategoryId = request.CategoryId,
        };

        if (request.ImageFile is { Length: > 0 })
        { 
           var imageName = await objectStorage.UploadFile(request.ImageFile, ObjectStorageTypes.MenuItem);
           menuItem.ImageName = imageName;
        }

        context.MenuItems.Add(menuItem);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateCategory(int categoryId, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        if (await context.Categories.AnyAsync(x => x.Id == categoryId && x.ShopId == shopId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Category_NotFound);
        }
    }
}