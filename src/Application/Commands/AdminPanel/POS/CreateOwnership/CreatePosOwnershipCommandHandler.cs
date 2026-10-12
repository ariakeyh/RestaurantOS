using Application.Interface;
using Common;
using Common.Exceptions;
using Domain.Entities.Auth;
using Domain.Entities.Shops;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.POS.CreateOwnership;

public class CreatePosOwnershipCommandHandler(
    IAppDbContext dbContext,
    ICacheService cacheService,
    IAuthService authService) : IRequestHandler<CreatePosOwnershipCommand, CreatePosOwnershipResult>
{
    public async Task<CreatePosOwnershipResult> Handle(CreatePosOwnershipCommand request,
        CancellationToken cancellationToken)
    {
        var (redisKey, currentShopId) = await CheckToken(request);

        var device = await HandlePosDevice(request, currentShopId, cancellationToken);

        await cacheService.Delete(redisKey);

        var jwt = authService.Login(device);

        return new CreatePosOwnershipResult(jwt);
    }

    private async Task<POSDevice> HandlePosDevice(CreatePosOwnershipCommand request,
        int currentShopId,
        CancellationToken cancellationToken)
    {
        var device =
            await dbContext.POSDevices.FirstOrDefaultAsync(
                x => x.IMEI == request.Imei && x.SerialNumber == request.SerialNumber && x.ShopId == currentShopId,
                cancellationToken);

        if (device != null)
        {
            throw new BadRequestException(ErrorMessages.POS_Already_Assigned);
        }

        device = new POSDevice
        {
            IMEI = request.Imei,
            SerialNumber = request.SerialNumber,
            ShopId = currentShopId,
            Type = ShopUserType.POS,
            User = new User
            {
                Name = "POS-" + Guid.NewGuid().ToString().Take(5)
            }
        };
        dbContext.POSDevices.Add(device);
        await dbContext.SaveChangesAsync(cancellationToken);
        return device;
    }

    private async Task<(string redisKey, int currentShopId)> CheckToken(CreatePosOwnershipCommand request)
    {
        var redisKey = $"pos:token:{request.Token}";
        var shopIdFromToken = await cacheService.GetValue<string>(redisKey);
        if (shopIdFromToken == null)
            throw new UnauthorizedAccessException(ErrorMessages.POS_Invalid_Token);

        var currentShopId = authService.GetShopId();

        return shopIdFromToken != currentShopId.ToString()
            ? throw new BadRequestException(ErrorMessages.POS_Invalid_Token)
            : (redisKey, currentShopId);
    }
}