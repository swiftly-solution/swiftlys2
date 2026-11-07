using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreTranslationServiceInjection
{
    public static IServiceCollection AddCoreTranslationService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CoreTranslationService>();
        return self;
    }

    public static void UseCoreTranslationService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CoreTranslationService>();
    }
}
