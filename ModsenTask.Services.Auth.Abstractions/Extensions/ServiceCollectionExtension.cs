using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ModsenTask.Services.Auth.Abstractions.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}