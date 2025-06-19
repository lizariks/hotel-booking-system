
using System.Collections.Concurrent;
using System.Security.Cryptography;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services.Implement
{
    public class RefreshTokenService : IRefreshTokenService
    {
        
        private static readonly ConcurrentDictionary<string, string> _refreshTokens = new();

        public string GenerateRefreshToken(string userId)
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            _refreshTokens[userId] = refreshToken;
            return refreshToken;
        }

        public Task<bool> ValidateRefreshToken(string userId, string refreshToken)
        {
            if (_refreshTokens.TryGetValue(userId, out var storedToken))
            {
                return Task.FromResult(storedToken == refreshToken);
            }
            return Task.FromResult(false);
        }
    }
}
