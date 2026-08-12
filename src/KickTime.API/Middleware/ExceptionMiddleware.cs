using System.Net;
using System.Text.Json;
using KickTime.API.Contracts;

namespace KickTime.API.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred while processing {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,

            UnauthorizedAccessException => HttpStatusCode.Unauthorized,

            KeyNotFoundException => HttpStatusCode.NotFound,

            InvalidOperationException => HttpStatusCode.BadRequest,

            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.Fail(
            exception is InvalidOperationException or
            ArgumentException or
            KeyNotFoundException or
            UnauthorizedAccessException
                ? exception.Message
                : "An unexpected error occurred.");

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}