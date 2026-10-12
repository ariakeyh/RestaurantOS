using MediatR;

namespace Application.Queries.AdminPanel.POS;

public sealed record GetPosDevicesCommand : IRequest<List<PosDeviceDto>>;

public class PosDeviceDto
{
    public string IMEI { get; set; }
    public string SerialNumber { get; set; }
    public DateTime CreateDate { get; set; }
}