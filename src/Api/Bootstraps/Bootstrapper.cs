using System.Text;
using System.Threading.RateLimiting;
using Api.BackgroundServices;
using Api.Helpers;
using Api.Middlewares;
using API.Services;
using Application.Commands.AdminPanel.Categories.Create;
using Application.Interface;
using FluentValidation;
using Infra.Shlink;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Api.Bootstraps;

public static class Bootstrapper
{
    internal const string CorsPolicy = "CORS";
    
    public static WebApplication BootstrapServices(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration).WriteTo.Console()
                .Filter.ByExcluding(logEvent =>
                    logEvent.Properties.ContainsKey("RequestMethod") &&
                    logEvent.Properties["RequestMethod"].ToString()
                        .Contains("OPTIONS", StringComparison.OrdinalIgnoreCase)
                );
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // No issuer validation
                    ValidateAudience = false, // No audience validation 
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!))
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/hub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));
        builder.Services.AddHttpContextAccessor();
        AddServices(builder);
        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
        builder.Services.AddHttpClient();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicy,
                builder =>
                {
                    builder.WithOrigins(
                            "https://api.menu.encel.ir",
                            "https://admin.menu.encel.ir",
                            "http://localhost:5173"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });
        builder.Services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = !builder.Environment.IsProduction();
            options.MaximumReceiveMessageSize = 32 * 1024;
            options.StreamBufferCapacity = 10;
            options.HandshakeTimeout = TimeSpan.FromSeconds(15);
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        });
        builder.Services.AddScoped<INotifierService, NotifierService>();
        AddQuartz(builder);
        AddRateLimiter(builder);
        

        return builder.Build();
    }

    private static void AddRateLimiter(WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                var authService = httpContext.RequestServices.GetRequiredService<IAuthService>();

                var requestInfo = authService.GetCurrentRequestInfo();
                var userId = requestInfo.UserKey.ToString();

                return RateLimitPartition.GetSlidingWindowLimiter(userId, _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 40,
                    Window = TimeSpan.FromMinutes(1),
                    SegmentsPerWindow = 4,
                    QueueLimit = 3,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                });
            });

            options.AddPolicy("StrictPolicy", httpContext =>
            {
                var authService = httpContext.RequestServices.GetRequiredService<IAuthService>();

                var requestInfo = authService.GetCurrentRequestInfo();
                var userId = requestInfo.UserKey.ToString();

                return RateLimitPartition.GetSlidingWindowLimiter(userId, _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    SegmentsPerWindow = 4
                });
            });
        });
    }

    private static void AddQuartz(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();
        builder.Services.AddQuartz(q =>
        {
            var jobKey = new JobKey("NotificationJob");

            q.AddJob<NotifierBackgroundJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("NotificationJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever()));
            
            var ownerNotifierJobKey = new JobKey("NotifyOwnerJob");
            
            q.AddJob<NotifyOwnerBackgroundJob>(opt => opt.WithIdentity(ownerNotifierJobKey));
            
            q.AddTrigger(opts => opts
                .ForJob(ownerNotifierJobKey)
                .WithIdentity("NotifyOwnerJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10)
                    .RepeatForever()));
        });

        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<CaptchaValidationAttribute>();
        builder.Services.AddRateLimits(builder.Environment);
    }
}