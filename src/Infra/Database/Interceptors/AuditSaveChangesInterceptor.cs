using Application.Interface;
using Domain.Entities.Agg;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Database.Interceptors;

public class AuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var userId = 0;

        if (httpContextAccessor.HttpContext is not null)
        {
            var authService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IAuthService>();
            userId = authService.GetUserId();
        }

        ApplyAuditInfo(eventData.Context, userId);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var authService = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IAuthService>();
        ApplyAuditInfo(eventData.Context, authService.GetUserId());
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAuditInfo(DbContext? context, int userId)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries()
            .Where(e => e is { Entity: AggregateRoot, State: EntityState.Added or EntityState.Modified });

        var now = DateTime.Now;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(nameof(AggregateRoot.CreatedAt)).CurrentValue = now;
                    entry.Property(nameof(AggregateRoot.CreatorId)).CurrentValue = userId;
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(AggregateRoot.EditedAt)).CurrentValue = now;
                    entry.Property(nameof(AggregateRoot.EditorId)).CurrentValue = userId;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Property(nameof(AggregateRoot.IsDeleted)).CurrentValue = true;
                    entry.Property(nameof(AggregateRoot.EditedAt)).CurrentValue = now;
                    entry.Property(nameof(AggregateRoot.EditorId)).CurrentValue = userId;
                    break;
            }
        }
    }
}