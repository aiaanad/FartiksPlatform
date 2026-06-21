namespace FartiksPlatform.Services.User.Application.Interfaces;

public interface IPasswordHashGenerator
{
    string GenerateHash(string password);
    bool VerifyHash(string password, string hash);
}
