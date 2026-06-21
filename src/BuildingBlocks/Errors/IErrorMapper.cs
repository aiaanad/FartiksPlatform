namespace BuildingBlocks.Errors;

public interface IErrorMapper
{
    (int StatusCode, string ErrorType, string Title)? Map(Exception exception);
}
