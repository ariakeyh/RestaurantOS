using Application.Interface;
using Infra.Database;
using Infra.Database.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Acceptance;

public class TestServerFixture : WebApplicationFactory<Api.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var testDb = Environment.GetEnvironmentVariable("MENU_TEST_DB");

        if (testDb is null || string.IsNullOrWhiteSpace(testDb))
        {
            builder.UseEnvironment("IntegrationTest-dev");

            builder.ConfigureServices(services =>
            {
                var descriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>))
                    .ToList();
                
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                var testServerName = Guid.NewGuid().ToString();
                services.AddSingleton<AuditSaveChangesInterceptor>();
                services.AddDbContext<AppDbContext>((sp, options) =>
                {
                    var interceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();
                    options.AddInterceptors(interceptor).UseInMemoryDatabase(testServerName);
                });
                services.AddScoped<IAppDbContext, AppDbContext>();
            });
        }
        else
        {
            builder.UseEnvironment("IntegrationTest");
        }
        
        // builder.ConfigureServices(services =>
        // {
        //     services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AdminLoginRequest).Assembly)); 
        // });
    }
}