using KickTime.API.Contracts;
using KickTime.Application.Exceptions;
using System.Net;
using System.Text.Json;

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
            ValidationException => HttpStatusCode.BadRequest,

            UnauthorizedException => HttpStatusCode.Unauthorized,

            NotFoundException => HttpStatusCode.NotFound,

            ConflictException => HttpStatusCode.Conflict,

            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var message = exception switch
        {
            ValidationException => exception.Message,

            UnauthorizedException => exception.Message,

            NotFoundException => exception.Message,

            ConflictException => exception.Message,

            _ => "An unexpected error occurred."
        };

        var errors = exception is ValidationException validationException
            ? validationException.Errors
            : null;

        var response = ApiResponse<object>.Fail(
            message,
            errors);

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}