using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.DTO;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(
            IUserService userService,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.RegisterAsync(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.LoginAsync(model);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);
            var refreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshDto dto)
        {
            var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
                return BadRequest("Invalid access token");

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var isValid = await _refreshTokenService.ValidateRefreshToken(userId, dto.RefreshToken);
            if (!isValid)
                return Unauthorized("Invalid refresh token");

            var newAccessToken = _jwtTokenGenerator.GenerateToken(userId, principal.FindFirstValue(ClaimTypes.Email));
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken(userId); // Rotate token

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserByIdAsync(userId);

            return user == null ? NotFound() : Ok(user);
        }
    }
}
