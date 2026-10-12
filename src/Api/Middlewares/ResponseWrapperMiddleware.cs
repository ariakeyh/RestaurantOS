using System.Net;
using Common.Exceptions;
using Newtonsoft.Json;

namespace Api.Middlewares;

public class ResponseWrapperMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var newBodyStream = new MemoryStream();
        try
        {
            if (context.Request.Path.Value.Contains("hub") ||context.Request.Path.Value.Contains("scalar") || context.Request.Path.Value.Contains("openapi"))
            {
                await next(context);
                return;
            }
            
            context.Response.Body = newBodyStream;
            await next(context);


            newBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
            newBodyStream.Seek(0, SeekOrigin.Begin);

            var apiResponse = GenerateResponse(context, responseBody);

            context.Response.ContentType = "application/json";

            var jsonResponse = JsonConvert.SerializeObject(apiResponse);
            await context.Response.WriteAsync(jsonResponse);

            newBodyStream.Seek(0, SeekOrigin.Begin);
            await newBodyStream.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, originalBodyStream, newBodyStream);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private static ApiResponse<object> GenerateResponse(HttpContext context, string responseBody)
    {
        var statusCode = context.Response.StatusCode;
        var message = statusCode >= 400 ? "FAILURE" : "SUCCESS";

        object parsedBody;

        try
        {
            parsedBody = JsonConvert.DeserializeObject<object>(responseBody);
        }
        catch (JsonException)
        {
            // If it's not valid JSON, just use the raw string
            parsedBody = responseBody;
        }
        
        var apiResponse = new ApiResponse<object>(
            message,
            statusCode,
            parsedBody ?? string.Empty
        );

        return apiResponse;
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, Stream originalBodyStream,
        Stream newBodyStream)
    {
        HttpStatusCode status;
        string message;

        switch (exception)
        {
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case BadRequestException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case DuplicateException:
                status = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;
            case TooManyRequestsException:
                status = HttpStatusCode.TooManyRequests;
                message = exception.Message;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var apiErrorResponse = new ApiResponse<string>(
            message,
            (int)status,
            string.Empty
        );

        context.Response.ContentType = "application/json";

        // Write the new response
        var jsonResponse = JsonConvert.SerializeObject(apiErrorResponse);
        await context.Response.WriteAsync(jsonResponse);

        // Copy the original stream contents back to the response stream
        newBodyStream.Seek(0, SeekOrigin.Begin);
        await newBodyStream.CopyToAsync(originalBodyStream);
    }
}