using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Menu;
using SwiftlyS2.Core.Menu.Config;
using SwiftlyS2.Core.Menu.Renderers;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuInjection
{
    public static IServiceCollection AddMenu( this IServiceCollection self )
    {
        _ = self.AddSingleton<MenuActionRegistry>();
        _ = self.AddSingleton<MenuRendererRegistry>();
        _ = self.AddSingleton<MenuKeybindResolver>();
        _ = self.AddSingleton<MenuLayoutOptions>();
        _ = self.AddSingleton<MenuRenderDiagnostics>();
        _ = self.AddSingleton<MenuChatCapture>();
        _ = self.AddSingleton<MenuSoundPlayer>();
        _ = self.AddSingleton<MenuFrameComposer>();
        _ = self.AddSingleton<MenuRuntime>();
        _ = self.AddSingleton<MenuInputRouter>();
        _ = self.AddSingleton<GlobalMenuKeybindSource>();
        _ = self.AddSingleton<CenterHtmlMenuRenderer>();
        _ = self.AddSingleton<ChatMenuRenderer>();
        _ = self.AddSingleton<ChatColoredTextComponentRenderer>();
        return self;
    }
}
