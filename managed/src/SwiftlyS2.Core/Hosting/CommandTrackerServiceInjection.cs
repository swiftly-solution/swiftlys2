using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Engine;

namespace SwiftlyS2.Core.Hosting;

internal static class CommandTrackerServiceInjection
{
    public static IServiceCollection AddCommandTrackerService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CommandTrackerService>();
        return self;
    }

    public static void UseCommandTrackerService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CommandTrackerService>();
    }
}