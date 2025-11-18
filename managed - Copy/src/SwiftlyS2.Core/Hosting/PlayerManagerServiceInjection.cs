using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Players;

namespace SwiftlyS2.Core.Hosting;

internal static class PlayerManagerServiceInjection
{
    public static IServiceCollection AddPlayerManagerService(this IServiceCollection self)
    {
        return self.AddSingleton<PlayerManagerService>();
    }
}