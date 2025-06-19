using System.Threading.Tasks;

namespace WebApplication2.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken(string userId);
        Task<bool> ValidateRefreshToken(string userId, string refreshToken);
    }
}