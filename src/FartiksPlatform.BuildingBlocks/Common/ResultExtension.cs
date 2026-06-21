using FartiksPlatform.BuildingBlocks.Errors;

namespace FartiksPlatform.BuildingBlocks.Common;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Common.Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        int statusCode = result.Error.Code switch
        {
            ErrorTypes.InvalidCredentials or ErrorTypes.ValidationError => StatusCodes.Status400BadRequest,
            ErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorTypes.UserNotFound or ErrorTypes.GameNotFound => StatusCodes.Status404NotFound,
            ErrorTypes.InsufficientFunds => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        var errorDto = new ErrorDto
        {
            Title = "Business fault",
            Status = statusCode,
            ErrorType = result.Error.Code,
            Detail = result.Error.Message,
            Timestamp = DateTime.UtcNow,
            TraceId = "BUSINESS_FAULT"
        };

        return new ObjectResult(errorDto) { StatusCode = statusCode };
    }
}
