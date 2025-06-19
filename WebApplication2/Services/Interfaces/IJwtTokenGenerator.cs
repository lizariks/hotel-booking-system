namespace WebApplication2.Services.Interfaces;
using System.Security.Claims;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
