using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Database;
using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Menu;
using SwiftlyS2.Core.Menu.Config;
using SwiftlyS2.Core.Menu.Renderers;
using SwiftlyS2.Core.Menus;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Core.Permissions;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.Plugins;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreServices
{
    private static readonly Type[] Singletons =
    [
        typeof(ProfileService),
        typeof(ConfigurationService),
        typeof(RootDirService),
        typeof(DataDirectoryService),
        typeof(CoreTranslationService),
        typeof(PluginManager),
        typeof(HookManager),
        typeof(TraceManager),
        typeof(PermissionManager),
        typeof(DatabaseConnectionManager),
        typeof(CoreHookService),
        typeof(CoreVoiceService),
        typeof(CorePlayerService),
        typeof(GameSystemService),
        typeof(CoreCommandService),
        typeof(MenuManagerAPI),
        typeof(MenuManagerAPIService),
        typeof(MenuRuntimeService),
        typeof(PlayerManagerService),
        // Menu internals
        typeof(MenuActionRegistry),
        typeof(MenuRendererRegistry),
        typeof(MenuKeybindResolver),
        typeof(MenuRendererResolver),
        typeof(MenuInputMethodResolver),
        typeof(MenuLayoutOptions),
        typeof(MenuRenderDiagnostics),
        typeof(MenuChatCapture),
        typeof(MenuSoundPlayer),
        typeof(MenuFrameComposer),
        typeof(MenuRenderWorker),
        typeof(MenuRuntime),
        typeof(MenuInputRouter),
        typeof(GlobalMenuKeybindSource),
        typeof(GlobalMenuRendererSource),
        typeof(GlobalMenuInputMethodSource),
        typeof(CenterHtmlMenuRenderer),
        typeof(ChatMenuRenderer),
        typeof(ChatColoredTextComponentRenderer),
    ];

    private static readonly Type[] Eager =
    [
        typeof(CoreHookService),
        typeof(CoreVoiceService),
        typeof(CoreTranslationService),
        typeof(CorePlayerService),
        typeof(GameSystemService),
        typeof(PermissionManager),
        typeof(MenuManagerAPIService),
        typeof(MenuRuntimeService),
        typeof(CoreCommandService),
    ];

    public static IServiceCollection AddCoreServices( this IServiceCollection self, string basePath )
    {
        foreach (var type in Singletons)
        {
            _ = self.AddSingleton(type);
        }

        _ = self.AddOptions<CommandOverrideConfig>().BindConfiguration("CommandOverrides").ValidateOnStart();
        _ = self.AddOptions<PermissionConfig>().BindConfiguration("Permissions").ValidateOnStart();

        _ = self.AddSingleton<ISwiftlyCore, SwiftlyCore>(provider => new SwiftlyCore(
            "SwiftlyS2",
            basePath,
            null,
            typeof(SwiftlyCore),
            provider,
            basePath
        ));
        return self;
    }

    public static void StartCoreServices( this IServiceProvider self )
    {
        var core = self.GetRequiredService<ISwiftlyCore>();
        GameHooksPublisher.Initialize(core);
        DatamapHooksPublisher.Initialize(core);

        foreach (var type in Eager)
        {
            _ = self.GetRequiredService(type);
        }

        self.GetRequiredService<PluginManager>().Initialize();
    }
}
