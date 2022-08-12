using Microsoft.Extensions.DependencyInjection;
using ModsenTask.Services.Auth.Abstractions.Interfaces;
using ModsenTask.Services.Auth.Implementation;

namespace ModsenTask.Services.Auth.Extenstions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        => services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>();
    
    public static IServiceCollection AddUser(this IServiceCollection services)
        => services
            .AddScoped<IUserService, UserService>();
}