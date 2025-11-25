using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Plugins;

namespace SwiftlyS2.Core.Hosting;

internal static class PluginManagerInjection
{
    public static IServiceCollection AddPluginManager( this IServiceCollection self )
    {
        _ = self.AddSingleton<PluginManager>();
        return self;
    }

    public static void UsePluginManager( this IServiceProvider self )
    {
        _ = self.GetRequiredService<PluginManager>();
    }
}