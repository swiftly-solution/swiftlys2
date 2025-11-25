using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class ConfigurationServiceInjection
{
    public static IServiceCollection AddConfigurationService( this IServiceCollection self )
    {
        _ = self.AddSingleton<ConfigurationService>();
        return self;
    }
}