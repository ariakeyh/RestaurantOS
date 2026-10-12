using Application.Interface;
using Domain.Entities.Sms;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundJobs;

public sealed class SmsDispatchBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<SmsDispatchBackgroundService> logger)
    : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(2);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Sms dispatch background service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DispatchAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }

    private async Task DispatchAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        var smsService = scope.ServiceProvider.GetRequiredService<ISmsService>();

        var now = DateTime.UtcNow;

        var receptors = await context.SmsRequestReceptors
            .Include(x => x.SmsRequest)
            .Where(x =>
                x.Status == SmsReceptorStatus.Scheduled &&
                x.SendDate <= now &&
                x.SmsRequest.Status == SmsRequestStatus.Approved)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        if (receptors.Count == 0)
        {
            logger.LogInformation("Found zero sms receptors to send message to.");
            return;
        }

        foreach (var receptor in receptors)
        {
            await TrySendAsync(receptor, smsService);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task TrySendAsync(
        SmsRequestReceptor receptor,
        ISmsService smsService)
    {
        var request = receptor.SmsRequest;

        try
        {
            await smsService.Send(
                lineNumber: default, // TODO get from appsettings 
                message: receptor.Message,
                numbers: new[] { receptor.ReceptorNumber });
            
            receptor.SetStatus(SmsReceptorStatus.SentToProvider);

            request.IncreaseSend();
        }
        catch (Exception ex)
        {
            logger.LogWarning(
                ex,
                "SMS send attempt failed for receptor {ReceptorId}, will retry",
                receptor.Id);
        }
    }
}