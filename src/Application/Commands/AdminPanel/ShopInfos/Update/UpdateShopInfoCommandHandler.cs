using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.ShopInfos.Update;

public class UpdateShopCommandHandler(IAppDbContext context, IAuthService authService, IObjectStorage objectStorage)
    : IRequestHandler<UpdateShopCommand>
{
    public async Task Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();
        var requestNameTrimmed = request.Name.Trim();
        var shopInfo = await context.Shops.FirstOrDefaultAsync(x=>x.Id == shopId,cancellationToken);
        
        if(shopInfo is null)
            throw new NotFoundException(ErrorMessages.NotFound);
        
        shopInfo.Name = requestNameTrimmed;
        shopInfo.NumberOfTables = request.NumberOfTables;
        shopInfo.Description = request.Description;
        shopInfo.ContactInfo = request.ContactInfo;
        shopInfo.Address = request.Address;

        if (request.ImageFile is { Length: > 0 })
        {
            var imageName = await objectStorage.UploadFile(request.ImageFile, ObjectStorageTypes.Shop);
            shopInfo.ImageName = imageName;
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}