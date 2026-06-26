using System.Security.Cryptography;  
using FartiksPlatform.Services.User.Application.Interfaces;  
  
namespace FartiksPlatform.Services.User.Infrastructure.Security;  
  
public class PasswordHashGenerator : IPasswordHashGenerator  
{  
    private const int SaltSize = 16;  
    private const int KeySize = 32;  
    private const int Iterations = 100_000;  
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;  
  
    public string GenerateHash(string password)  
    {  
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);  
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);  
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";  
    }  
  
    public bool VerifyHash(string password, string hash)  
    {  
        var parts = hash.Split('.', 2);  
        if (parts.Length != 2)  
            return false;  
  
        byte[] salt = Convert.FromBase64String(parts[0]);  
        byte[] expected = Convert.FromBase64String(parts[1]);  
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, expected.Length);  
        return CryptographicOperations.FixedTimeEquals(actual, expected);  
    }  
}
