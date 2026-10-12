using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Middlewares;

[AttributeUsage(AttributeTargets.Method)]
public class CaptchaValidationAttribute : ActionFilterAttribute
{
    private const string CaptchaHeaderName = "Captcha-Response";
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var captchaResponse = httpContext.Request.Headers[CaptchaHeaderName].ToString();

        if (string.IsNullOrEmpty(captchaResponse))
        {
            context.Result = new BadRequestObjectResult(new { message = "CAPTCHA response is required." });
            return;
        }

        var captchaService = httpContext.RequestServices.GetRequiredService<ICaptchaService>();

        await captchaService.VerifyAsync(captchaResponse);

        await next();
    }
}