namespace WebApplication2.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email);
}
