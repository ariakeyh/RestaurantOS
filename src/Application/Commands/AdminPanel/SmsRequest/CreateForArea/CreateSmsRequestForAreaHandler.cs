using Application.Interface;
using Application.Services;
using Domain.Enums;
using MediatR;

namespace Application.Commands.AdminPanel.SmsRequest.CreateForArea;

public class CreateSmsRequestForAreaHandler(IAuthService authService, SmsSchedulerService smsScheduler) : IRequestHandler<CreateSmsRequestForAreaCommand, CreateSmsRequestForAreaResult>
{
    public async Task<CreateSmsRequestForAreaResult> Handle(CreateSmsRequestForAreaCommand request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        var smsRequestId = await smsScheduler.Schedule(request.Message, SmsRequestType.Area, currentShopId, request.ReceptorsCount, request.Payload);
        return new CreateSmsRequestForAreaResult(smsRequestId);
    }
}