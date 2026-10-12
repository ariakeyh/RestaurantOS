using Api.Hubs;
using Api.Middlewares;
using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

namespace Api.Bootstraps;

public static class Wrapper
{
    public static async Task WrapUp(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseCors(Bootstrapper.CorsPolicy);
        if (app.Environment.IsDevelopment())
        {
            app.MapScalar();

            await app.MigrateDatabase();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseRateLimiter();
        

        app.MapControllers();
        app.UseMiddleware<ResponseWrapperMiddleware>();
        app.MapHub<NotifierHub>("/hub/warner-notifier");
    }

    private static void MapScalar(this WebApplication app)
    {
        app.MapScalarApiReference(opt =>
        {
            opt.Servers = [];
            opt.Title = "Menu APIs";
            opt.Theme = ScalarTheme.Moon;
            opt.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);

            opt.Authentication = new ScalarAuthenticationOptions
            {
                PreferredSecurityScheme = "Bearer"
            };
        });
        app.MapOpenApi();
    }

    private static async Task MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<AppDbContext>();

        if (!app.Environment.EnvironmentName.Contains("test", StringComparison.CurrentCultureIgnoreCase))
        {
            await db.Database.MigrateAsync(CancellationToken.None);
        }
    }
}