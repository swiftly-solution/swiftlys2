using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreVoiceServiceInjection
{
    public static IServiceCollection AddCoreVoiceService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CoreVoiceService>();
        return self;
    }

    public static void UseCoreVoiceService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CoreVoiceService>();
    }
}
