using Application.Interface;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundJobs;

public class SmsStatusPollingJob(IServiceScopeFactory scopeFactory,ILogger<SmsStatusPollingJob> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(2);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Sms dispatch background service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PollSmsStatusAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }

    private async Task PollSmsStatusAsync(CancellationToken cancellationToken = default)
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        var smsService = scope.ServiceProvider.GetRequiredService<ISmsService>();

        var requests = await context.SmsRequests
            .Include(r => r.Receptors)
            .Where(r =>
                r.Receptors.Any(x => x.Status == SmsReceptorStatus.SentToProvider))
            .ToListAsync(cancellationToken);

        foreach (var request in requests)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.ProviderPackId))
                {
                    var results = await smsService.GetPackStatus(request.ProviderPackId!);

                    var resultMap = results.ToDictionary(r => r.ProviderMessageId);

                    foreach (var receptor in request.Receptors)
                    {
                        if (!receptor.ProviderMessageId.HasValue) continue;
                        if (!resultMap.TryGetValue(receptor.ProviderMessageId.Value, out var providerResult)) continue;

                        receptor.SetStatus(providerResult.Status, providerResult.ProviderFinalDeliveryState);
                    }
                }
                else
                {
                    foreach (var receptor in request.Receptors)
                    {
                        if (!receptor.ProviderMessageId.HasValue) continue;

                        var result = await smsService.GetStatus(receptor.ProviderMessageId.Value);
                        receptor.SetStatus(result.Status, result.ProviderFinalDeliveryState);
                    }
                }

                var totalSent = request.Receptors.Count(r => r.Status >= SmsReceptorStatus.SentToProvider);
                var totalDelivered = request.Receptors.Count(r => r.Status == SmsReceptorStatus.Delivered);
                var totalFailed = request.Receptors.Count(r => r.Status == SmsReceptorStatus.Undelivered);
                

                request.UpdateStatus(totalSent, totalDelivered, totalFailed);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error polling SMSRequest {request.Id}: {ex.Message}");
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
