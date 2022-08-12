using AestheticLife.Auth.Services.Abstractions.Models;
using ModsenTask.Services.Auth.Abstractions.Models;

namespace ModsenTask.Services.Auth.Abstractions.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterUserDto userDto);

    Task<TokenDto> LoginAsync(LoginDto dto);
}