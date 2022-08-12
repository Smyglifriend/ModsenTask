using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ModsenTask.Core.Abstractions.Constants;
using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.Services.Auth.Abstractions.Interfaces;
using ModsenTask.Services.Auth.Abstractions.Models;

namespace ModsenTask.Services.Auth.Implementation;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;


    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        ITokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }


    public async Task RegisterAsync(RegisterUserDto userDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(userDto.Email.ToUpper());
        if (existingUser is not null)
            throw new Exception("User with this email already exists");

        var user = _mapper.Map<User>(userDto);
        var registerResult = await _userManager.CreateAsync(user, userDto.Password);
        if (!registerResult.Succeeded)
            throw new Exception($"{registerResult.Errors}.");

        var addingToRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.ROLE_USER.ToUpper());
        if (!addingToRoleResult.Succeeded)
            throw new Exception($"An adding the user \"{user.UserName}\" with the email \"{user.Email}\" " +
                                $"to the role \"{RoleConstants.ROLE_USER}\" is failed.");
    }

    public async Task<TokenDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email.ToUpper());
        if (user is null)
            throw new Exception("User not found");

        var correctPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!correctPassword)
            throw new Exception("Wrong password");

        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessToken = await _tokenService.SetAccessTokenAsync(user);

        return new()
        {
            RefreshToken = refreshToken.RefreshTokenString,
            AccessToken = accessToken.AccessTokenString,
            RefreshTokenExpiresAt = refreshToken.ExpiresAt,
            AccessTokenExpiresAt = accessToken.ExpiresAt
        };
    }
}