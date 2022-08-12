using System.Reflection;

namespace ModsenTask.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}