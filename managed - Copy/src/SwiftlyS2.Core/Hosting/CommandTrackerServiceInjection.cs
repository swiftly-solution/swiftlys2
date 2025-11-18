using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Engine;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CommandTrackerServiceInjection
{
    public static IServiceCollection AddCommandTrackerService(this IServiceCollection self)
    {
        return self.AddSingleton<CommandTrackerService>();
    }

    public static void UseCommandTrackerService(this IServiceProvider self)
    {
        self.GetRequiredService<CommandTrackerService>();
    }
}