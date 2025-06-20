using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.DTO;
using WebApplication2.Enteties;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;
    private readonly IRefreshTokenService _refreshTokenService;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration config,
        IRefreshTokenService refreshTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, model.Role ?? "Customer");

        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized("Invalid credentials.");

        var accessToken = GenerateJwtToken(user, out DateTime expires);
        var refreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            accessToken,
            refreshToken,
            expiration = expires,
            email = user.Email,
            fullName = $"{user.FirstName} {user.LastName}",
            role = roles.FirstOrDefault()
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshDto dto)
    {
        var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
        if (principal == null)
            return BadRequest("Invalid access token");

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        var isValid = await _refreshTokenService.ValidateRefreshToken(userId, dto.RefreshToken);
        if (!isValid)
            return Unauthorized("Invalid refresh token");

        var email = principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByIdAsync(userId);
        var accessToken = GenerateJwtToken(user, out _);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken(user.Id); 

        return Ok(new
        {
            accessToken,
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            FullName = $"{user.FirstName} {user.LastName}",
            Roles = await _userManager.GetRolesAsync(user)
        });
    }
    
    private string GenerateJwtToken(User user, out DateTime expiration)
    {
        var roles = _userManager.GetRolesAsync(user).Result;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        expiration = DateTime.UtcNow.AddMinutes(60);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false, 
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
