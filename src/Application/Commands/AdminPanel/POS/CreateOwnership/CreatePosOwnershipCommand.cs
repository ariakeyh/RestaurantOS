using MediatR;

namespace Application.Commands.AdminPanel.POS.CreateOwnership;

public sealed record CreatePosOwnershipResult(string AccessToken);

public sealed record CreatePosOwnershipCommand(
    string Imei,
    string SerialNumber,
    string Token) : IRequest<CreatePosOwnershipResult>;

    
