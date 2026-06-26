using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Security.Cryptography;  
using System.Text;  
using FartiksPlatform.Services.User.Application.Interfaces;  
using FartiksPlatform.Services.User.Domain.Entities;  
using Microsoft.IdentityModel.Tokens;  
  
namespace FartiksPlatform.Services.User.Infrastructure.Security;  
  
public class JwtProvider : IJwtProvider  
{  
    private readonly JwtOptions _options;  
  
    public JwtProvider(JwtOptions options)  
    {  
        _options = options ?? throw new ArgumentNullException(nameof(options));  
    }  
  
    public string GenerateToken(AppUser user)  
    {  
        var claims = new[]  
        {  
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),  
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),  
            new Claim(ClaimTypes.Name, user.Username),  
            new Claim(ClaimTypes.Role, user.Role),  
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  
        };  
  
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));  
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  
  
        var token = new JwtSecurityToken(  
            issuer: _options.Issuer,  
            audience: _options.Audience,  
            claims: claims,  
            expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),  
            signingCredentials: credentials);  
  
        return new JwtSecurityTokenHandler().WriteToken(token);  
    }  
  
    public string RefreshToken(AppUser user)  
    {  
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));  
    }  
}
