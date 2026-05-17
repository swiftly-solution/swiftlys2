using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Hosting;

internal static class GameHooksPublisherInjection
{
    public static void UseGameHooksPublisher( this IServiceProvider self )
    {
        GameHooksPublisher.Initialize(self.GetRequiredService<ISwiftlyCore>());
    }
}
