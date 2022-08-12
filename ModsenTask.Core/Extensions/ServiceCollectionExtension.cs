using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsenTask.Core.Abstractions.Helpers;
using ModsenTask.Core.Helpers;

namespace ModsenTask.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHelpers(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddScoped<IConfigurationHelper>(di => new ConfigurationHelper(configuration));
}