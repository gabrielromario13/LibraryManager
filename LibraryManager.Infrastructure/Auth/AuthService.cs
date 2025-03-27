using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManager.Infrastructure.Auth;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public string ComputeHash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = SHA256.HashData(passwordBytes);

        var builder = new StringBuilder();

        foreach (var t in hashedBytes)
            builder.Append(t.ToString("x2"));

        return builder.ToString();
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Sub, "user_id"),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            null,
            DateTime.Now.AddHours(2),
            credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}