using Microsoft.AspNetCore.Identity;
using WebApplication2.DTO;
using WebApplication2.Enteties;
using WebApplication2.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<UserDto> RegisterAsync(UserRegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.UserName = dto.Email;

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> LoginAsync(UserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        return result.Succeeded ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}