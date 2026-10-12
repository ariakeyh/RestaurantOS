using Application.Interface;
using Application.Services;
using Application.Services.OrderServices;
using Application.Vault;
using Infra.Captcha;
using Infra.Database;
using Infra.Database.Interceptors;
using Infra.MinIo;
using Infra.Otp;
using Infra.Payment;
using Infra.Redis;
using Infra.Shlink;
using Infra.Sms;
using IPE.SmsIrClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using StackExchange.Redis;
using MinioConfig = Infra.MinIo.MinioConfig;

namespace Infra;

public static class Configure
{
    public static async Task AddInfraData(this IServiceCollection services, string environment)
    {
        services.AddSingleton<VaultProvider>();
        await AddServices(services);

        if (environment.Contains("test", StringComparison.CurrentCultureIgnoreCase)) return;

        RegisterDbContext(services);
    }

    private static Task AddServices(IServiceCollection services)
    {
        services.AddScoped<CustomerService>();
        services.AddScoped<UserService>();
        AddOrderServices(services);
        AddRedis(services);
        AddPollServices(services);
        AddSmsService(services);
        AddMinIoService(services);
        AddPaymentService(services);
        AddCaptchaService(services);
        AddShortLinkService(services);
        return Task.CompletedTask;
    }

    private static void AddPollServices(IServiceCollection services)
    {
        services.AddScoped<PollService>();
    }

    private static void AddOrderServices(IServiceCollection services)
    {
        services.AddScoped<POSOrderCreator>();
        services.AddScoped<WebsiteOrderCreator>();
        services.AddScoped<CashierOrderCreator>();
        services.AddScoped<OrderCreatorFactory>();
        services.AddScoped<OrderService>();
    }

    private static void AddRedis(IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var config = vault.FetchKV<RedisConfig>().Result;
            return config;
        });

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisConfig = sp.GetRequiredService<RedisConfig>();
            return ConnectionMultiplexer.Connect(redisConfig.ConnectionString);
        });

        services.AddSingleton<ICacheService, CacheService>();
    }


    private static void AddSmsService(IServiceCollection services)
    {
        services.AddSingleton<ISmsService, SmsIRService>();
        services.AddScoped<SmsPriceProviderService>();
        services.AddScoped<SmsSchedulerService>();
        services.AddSingleton<IOtpService, OtpService>();

        services.AddSingleton(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var config = vault.FetchKV<SmsProviderConfig>().Result;
            return config;
        });

        services.AddSingleton<SmsIr>(sp =>
        {
            var config = sp.GetRequiredService<SmsProviderConfig>();
            return new SmsIr(config.ApiKey);
        });
    }

    private static void AddMinIoService(IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var config = vault.FetchKV<MinioConfig>().Result;
            return config;
        });
        services.AddSingleton<IMinioClient>(sp =>
        {
            var minioConfig = sp.GetRequiredService<MinioConfig>();
            return new MinioClient()
                .WithEndpoint(minioConfig.Endpoint)
                .WithCredentials(minioConfig.AccessKey, minioConfig.SecretKey)
                .Build();
        });

        services.AddSingleton<IObjectStorage, ObjectStorage>();
    }

    private static void AddPaymentService(IServiceCollection services)
    {
        services.AddSingleton<ZarinpalConfig>(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var paymentConfig = vault.FetchKV<ZarinpalConfig>().Result;
            return paymentConfig;
        });

        services.AddScoped<IPaymentGatewayService, ZarinPalPaymentGatewayService>();
        services.AddScoped<InternalPaymentService>();
        services.AddScoped<ShopCreditService>();
    }

    private static void AddCaptchaService(IServiceCollection services)
    {
        services.AddSingleton<CaptchaConfig>(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var captchaConfig = vault.FetchKV<CaptchaConfig>().Result;
            return captchaConfig;
        });

        services.AddSingleton<ICaptchaService, CaptchaService>();
    }

    private static void AddShortLinkService(IServiceCollection services)
    {
        services.AddSingleton<ShlinkConfig>(sp =>
        {
            var vault = sp.GetRequiredService<VaultProvider>();
            var shlinkConfig = vault.FetchKV<ShlinkConfig>().Result;
            return shlinkConfig;
        });

        services.AddHttpClient<IShlinkService, ShlinkService>((sp, client) =>
        {
            var config = sp.GetRequiredService<ShlinkConfig>();

            client.BaseAddress = new Uri(config.Url);
            client.DefaultRequestHeaders.Add("X-Api-Key", config.ApiKey);
        });
    }

    private static void RegisterDbContext(IServiceCollection services)
    {
        services.AddSingleton<AuditSaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var vaultProvider = sp.GetRequiredService<VaultProvider>();
            var databaseConfig = vaultProvider.FetchKV<DatabaseConfig>().Result;

            var interceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();
            options.AddInterceptors(interceptor)
                .UseMySql(databaseConfig.ConnectionString,
                    ServerVersion.AutoDetect(databaseConfig.ConnectionString));
        });

        services.AddScoped<IAppDbContext, AppDbContext>();
    }
}