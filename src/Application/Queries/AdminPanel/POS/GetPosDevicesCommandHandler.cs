using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.POS;

public class GetPosDevicesCommandHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetPosDevicesCommand, List<PosDeviceDto>>
{
    public async Task<List<PosDeviceDto>> Handle(GetPosDevicesCommand request, CancellationToken cancellationToken)
    {
        var shopId = authService.GetShopId();

        var result = await context.POSDevices
            .OrderByDescending(x => x.Id)
            .Where(x => x.ShopId == shopId).Select(x => new PosDeviceDto
            {
                IMEI = x.IMEI,
                SerialNumber = x.SerialNumber,
                CreateDate = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return result;
    }
}