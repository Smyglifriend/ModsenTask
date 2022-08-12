using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ModsenTask.Commans.Event.Extensions;
using ModsenTask.Core.Abstractions.Helpers;
using ModsenTask.Core.Extensions;
using ModsenTask.DataAccess;
using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.DataAccess.Extensions;
using ModsenTask.DataAccess.Stores;
using ModsenTask.Queries.Event.Abstrations.Extensions;
using ModsenTask.Queries.Event.Extensions;
using ModsenTask.Services.Auth.Abstractions.Extensions;
using ModsenTask.Services.Auth.Extenstions;


namespace ModsenTask.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddHelpers(configuration)
            .AddMappers();

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddCustomRepositories();

    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddUser()
            .AddAuthenticationService()
            .AddQueryHandlers()
            .AddCommandHandlers();

    public static IServiceCollection ApplyCors(
        this IServiceCollection services)
    {
        var cHelper = services
            .BuildServiceProvider()
            .GetRequiredService<IConfigurationHelper>();

        return services
            .AddCors(opt => opt
                .AddPolicy(cHelper.Policy, policy => 
                    policy
                        .WithOrigins(cHelper.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
        => services
            .AddIdentityCore<User>(options =>
            {
                options.Tokens.ProviderMap.Add(
                    "Default",
                    new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<User>)));
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<ModsenTaskDbContext>()
            .AddDefaultTokenProvider()
            .Services
            .ConfgureJwt(config);

    public static string GetUsingCors(this IServiceCollection services)
        => services
            .BuildServiceProvider()
            .GetRequiredService<IConfigurationHelper>()
            .Policy;

    public static IServiceCollection ConfgureJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfig");
        var secretKey = jwtConfig["Secret"];
        return services
            .AddAuthentication(opt =>
            {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["ValidIssuer"],
                    ValidAudience = jwtConfig["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            })
            .Services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
        => services
            .AddAuthServicesMapper()
            .AddQueryMapper()
            .AddCommandMapper();
}