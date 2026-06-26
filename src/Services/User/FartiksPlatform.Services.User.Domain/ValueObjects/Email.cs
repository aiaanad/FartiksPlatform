using System.Net.Mail;

namespace FartiksPlatform.Services.User.Domain.ValueObjects;

public record Email
{
    public string Value { get; init; }

    private Email(string value) => Value = value;

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        try
        {
            var parsedAddress = new MailAddress(email);

            return new Email(parsedAddress.Address);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid email format.", nameof(email));
        }
    }
}
