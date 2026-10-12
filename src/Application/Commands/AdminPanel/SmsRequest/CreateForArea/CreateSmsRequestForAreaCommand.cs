using MediatR;

namespace Application.Commands.AdminPanel.SmsRequest.CreateForArea;

public sealed record CreateSmsRequestForAreaCommand(string Message, string Payload, int ReceptorsCount) : IRequest<CreateSmsRequestForAreaResult>;

public sealed record CreateSmsRequestForAreaResult(int SmsRequestId);