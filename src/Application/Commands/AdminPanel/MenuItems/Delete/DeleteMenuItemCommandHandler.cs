using Application.Interface;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.MenuItems.Delete;

public class DeleteMenuItemCommandHandler(IAppDbContext context, IObjectStorage objectStorage, IAuthService authService)
    : IRequestHandler<DeleteMenuItemCommand>
{
    public async Task Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var menuItem = await context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.Category.ShopId == shopId, cancellationToken);

        if (menuItem is null)
            throw new KeyNotFoundException(ErrorMessages.MenuItem_NotFound);

        var isDeleted = await objectStorage.DeleteFile(menuItem.Name);

        if (!isDeleted)
            throw new ApplicationException();
        
        context.MenuItems.Remove(menuItem);
        await context.SaveChangesAsync(cancellationToken);
    }
}