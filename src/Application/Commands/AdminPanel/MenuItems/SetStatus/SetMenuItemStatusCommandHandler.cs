using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.MenuItems.SetStatus;

public class SetMenuItemStatusCommandHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<SetMenuItemStatusCommand>
{
    public async Task Handle(SetMenuItemStatusCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var menuItem =
            await context.MenuItems.FirstOrDefaultAsync(x => x.Id == request.MenuItemId && x.Category.ShopId == shopId,
                cancellationToken);
        
        if (menuItem is null)
            throw new NotFoundException(ErrorMessages.MenuItem_NotFound);
        
        menuItem.IsUnAvailable = !menuItem.IsUnAvailable;

        await context.SaveChangesAsync(cancellationToken);
    }
}