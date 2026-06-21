using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Errors;

public class DefaultErrorMapper : IErrorMapper
{
    public (int StatusCode, string ErrorType, string Title)? Map(Exception exception)
    {
        switch (exception)
        {
            case UnauthorizedAccessException:
                return (StatusCodes.Status401Unauthorized, 
                        ErrorTypes.Unauthorized, 
                        "Неавторизовано");
            case System.ComponentModel.DataAnnotations.ValidationException:
                return (StatusCodes.Status400BadRequest, 
                        ErrorTypes.ValidationError,
                    "Ошибка валидации");
            default:
                return null;
        }
    }
}
