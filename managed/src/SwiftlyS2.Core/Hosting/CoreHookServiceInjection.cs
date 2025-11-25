using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreHookServiceInjection
{
    public static IServiceCollection AddCoreHookService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CoreHookService>();
        return self;
    }

    public static void UseCoreHookService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CoreHookService>();
    }
}