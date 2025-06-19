namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IUserService
{
    Task<UserDto> RegisterAsync(UserRegisterDto dto);
    Task<UserDto> LoginAsync(UserLoginDto dto);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserWithRolesDto?> GetUserWithRolesAsync(string userId);
    Task UpdateUserAsync(string id, string firstName, string lastName, string email);
    Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);

}