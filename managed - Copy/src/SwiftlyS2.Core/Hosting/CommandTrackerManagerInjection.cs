using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CommandTrackerManagerInjection
{
    public static IServiceCollection AddCommandTrackerManager(this IServiceCollection self)
    {
        return self.AddSingleton<CommandTrackerManager>();
    }

    
}