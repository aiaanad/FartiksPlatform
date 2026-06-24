using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FartiksPlatform.BuildingBlocks.Errors;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<IErrorMapper> _mappers;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IEnumerable<IErrorMapper> mappers,
        ILogger<ExceptionHandlingMiddleware> logger
        )
    {
        _next = next;
        _mappers = mappers;
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        (int StatusCode, string ErrorCode, string Title)? mapped = null;

        foreach (IErrorMapper mapper in _mappers)
        {
            mapped = mapper.Map(exception);
            if (mapped is not null)
            {
                break;
            }
        }

        int status;
        string errorType;
        string title;

        if (mapped != null)
        {
            status = mapped.Value.StatusCode;
            errorType = mapped.Value.ErrorCode;
            title = mapped.Value.Title;
        }
        else
        {
            status = StatusCodes.Status500InternalServerError;
            errorType = ErrorTypes.UnknownError;
            title = "Неизвестная ошибка";
        }

        string traceId = context.TraceIdentifier;
        _logger.LogError(exception,
            "Unhandled exception. Status: {Status}, ErrorType: {ErrorType}, TraceId: {TraceId}",
            status, errorType, traceId);

        ErrorDto error = new ()
        {
            Title = title,
            Status = status,
            ErrorType = errorType,
            Detail = exception.Message,
            TraceId = traceId,
            Timestamp = DateTime.UtcNow
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json; charset=utf-8";
        
        string answer = JsonSerializer.Serialize(error);
        await context.Response.WriteAsync(answer);
    }
}
