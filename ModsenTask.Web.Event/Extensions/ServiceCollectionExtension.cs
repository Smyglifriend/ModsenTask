using System.Reflection;

namespace ModsenTask.Web.Event.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEventWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}