using FartiksPlatform.Services.User.Application.Interfaces;

namespace FartiksPlatform.Services.User.Infrastructure.Security;

public class PasswordHashGenerator : IPasswordHashGenerator
{
    public string GenerateHash(string password)
    {
        throw new NotImplementedException();
    }

    public bool VerifyHash(string password, string hash)
    {
        throw new NotImplementedException();
    }
}
