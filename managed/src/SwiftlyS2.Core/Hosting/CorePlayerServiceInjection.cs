using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CorePlayerServiceInjection
{
    public static IServiceCollection AddCorePlayerService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CorePlayerService>();
        return self;
    }

    public static void UseCorePlayerService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CorePlayerService>();
    }
}
