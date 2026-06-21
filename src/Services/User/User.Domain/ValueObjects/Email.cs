namespace User.Domain.ValueObjects;

public record Email
{
    public string Value { get; init; } = string.Empty;
    public static Email Create(string email) => throw new NotImplementedException();
}
