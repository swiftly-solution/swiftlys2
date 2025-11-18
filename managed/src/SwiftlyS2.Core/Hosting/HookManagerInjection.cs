using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Hooks;

namespace SwiftlyS2.Core.Hosting;

internal static class HookServiceInjection
{
    public static IServiceCollection AddHookManager( this IServiceCollection self )
    {
        _ = self.AddSingleton<HookManager>();
        return self;
    }
}