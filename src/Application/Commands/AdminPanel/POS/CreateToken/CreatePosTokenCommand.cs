using MediatR;

namespace Application.Commands.AdminPanel.POS.CreateToken;

public sealed record CreatePosTokenResult(string Token);
public sealed record CreatePosTokenCommand : IRequest<CreatePosTokenResult>;