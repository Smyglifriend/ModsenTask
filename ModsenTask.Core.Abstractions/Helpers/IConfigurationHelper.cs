using Microsoft.Extensions.Configuration;

namespace ModsenTask.Core.Abstractions.Helpers;

public interface IConfigurationHelper
{
    IEnumerable<string> AllowedOrigins { get; }

    string Policy { get; }

    string Localhost { get; }

    IConfigurationSection JwtConfig { get; }
}