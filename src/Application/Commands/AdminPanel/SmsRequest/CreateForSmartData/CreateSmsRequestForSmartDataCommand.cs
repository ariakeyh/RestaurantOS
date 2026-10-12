using MediatR;

namespace Application.Commands.AdminPanel.SmsRequest.CreateForSmartData;

public sealed record CreateSmsRequestForSmartDataCommand(string Message, string Payload, int ReceptorsCount) : IRequest<CreateSmsRequestForSmartDataResult>;
public sealed record CreateSmsRequestForSmartDataResult(int SmsRequestId);