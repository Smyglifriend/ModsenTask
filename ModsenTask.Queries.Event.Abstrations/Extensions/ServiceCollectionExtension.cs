using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ModsenTask.Queries.Event.Abstrations.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQueryMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}