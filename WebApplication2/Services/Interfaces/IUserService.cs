namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IUserService
{
    Task<UserDto> RegisterAsync(UserRegisterDto dto);
    Task<UserDto> LoginAsync(UserLoginDto dto);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}