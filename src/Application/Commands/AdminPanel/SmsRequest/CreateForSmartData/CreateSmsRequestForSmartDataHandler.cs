using Application.Interface;
using Application.Services;
using Domain.Enums;
using MediatR;

namespace Application.Commands.AdminPanel.SmsRequest.CreateForSmartData;

public class CreateSmsRequestForSmartDataHandler(IAuthService authService, SmsSchedulerService smsScheduler) : IRequestHandler<CreateSmsRequestForSmartDataCommand, CreateSmsRequestForSmartDataResult>
{
    public async Task<CreateSmsRequestForSmartDataResult> Handle(CreateSmsRequestForSmartDataCommand request, CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();
        var smsRequestId = await smsScheduler.Schedule(request.Message, SmsRequestType.Area, currentShopId, request.ReceptorsCount, request.Payload);
        return new CreateSmsRequestForSmartDataResult(smsRequestId);
    }
}