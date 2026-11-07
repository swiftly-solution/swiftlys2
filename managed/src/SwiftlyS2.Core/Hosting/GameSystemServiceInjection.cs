using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class GameSystemServiceInjection
{
    public static IServiceCollection AddGameSystemService( this IServiceCollection self )
    {
        _ = self.AddSingleton<GameSystemService>();
        return self;
    }

    public static void UseGameSystemService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<GameSystemService>();
    }
}
