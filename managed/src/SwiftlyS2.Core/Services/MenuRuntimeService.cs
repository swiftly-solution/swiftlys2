using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Core.Menu;
using SwiftlyS2.Core.Menu.Config;
using SwiftlyS2.Core.Menu.Renderers;

namespace SwiftlyS2.Core.Services;

internal sealed class MenuRuntimeService : IDisposable
{
    private const string CoreOwner = "__swiftlys2.core__";

    private readonly ISwiftlyCore core;
    private readonly MenuRuntime runtime;
    private readonly MenuInputRouter router;
    private readonly MenuRenderWorker renderWorker;

    public MenuRuntimeService(
        ISwiftlyCore core,
        MenuRuntime runtime,
        MenuInputRouter router,
        MenuRenderWorker renderWorker,
        MenuChatCapture chat,
        MenuActionRegistry actions,
        MenuKeybindResolver resolver,
        GlobalMenuKeybindSource globalKeybinds,
        MenuRendererResolver rendererResolver,
        GlobalMenuRendererSource globalRendererSource,
        MenuInputMethodResolver inputMethodResolver,
        GlobalMenuInputMethodSource globalInputMethodSource,
        MenuRendererRegistry renderers,
        CenterHtmlMenuRenderer centerHtmlRenderer,
        ChatMenuRenderer chatMenuRenderer,
        ChatColoredTextComponentRenderer chatColoredTextComponentRenderer )
    {
        this.core = core;
        this.runtime = runtime;
        this.router = router;
        this.renderWorker = renderWorker;

        renderWorker.Start();
        RegisterCoreActions(actions);
        resolver.AddSource(globalKeybinds);
        rendererResolver.AddSource(globalRendererSource);
        inputMethodResolver.AddSource(globalInputMethodSource);
        _ = renderers.Register(centerHtmlRenderer, CoreOwner);
        _ = renderers.Register(chatMenuRenderer, CoreOwner);
        _ = renderers.RegisterComponentRenderer(chatColoredTextComponentRenderer, CoreOwner);
        chat.Attach(core.Command, router.OnClientChat);

        core.Event.OnClientKeyStateChanged += router.OnClientKeyStateChanged;
        core.Event.OnClientDisconnected += OnClientDisconnected;
        core.Event.OnTick += runtime.OnTick;
    }

    private static void RegisterCoreActions( MenuActionRegistry actions )
    {
        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.NavigateUp,
            DefaultKey = MenuKey.Shift,
            Label = "Up",
            Order = 0
        }, CoreOwner);

        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.NavigateDown,
            DefaultKey = MenuKey.F,
            Label = "Down",
            Order = 1
        }, CoreOwner);

        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.NavigateLeft,
            DefaultKey = MenuKey.A,
            Label = "Left",
            ShowInFooter = false,
            Order = 2
        }, CoreOwner);

        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.NavigateRight,
            DefaultKey = MenuKey.D,
            Label = "Right",
            ShowInFooter = false,
            Order = 3
        }, CoreOwner);

        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.Select,
            DefaultKey = MenuKey.E,
            Label = "Select",
            Order = 4
        }, CoreOwner);

        _ = actions.Register(new MenuActionDescriptor {
            Id = MenuActions.Close,
            DefaultKey = MenuKey.Tab,
            Label = "Close",
            Order = 5
        }, CoreOwner);
    }

    private void OnClientDisconnected( IOnClientDisconnectedEvent @event ) => runtime.OnClientDisconnected(@event.PlayerId);

    public void Dispose()
    {
        core.Event.OnClientKeyStateChanged -= router.OnClientKeyStateChanged;
        core.Event.OnClientDisconnected -= OnClientDisconnected;
        core.Event.OnTick -= runtime.OnTick;
        renderWorker.Dispose();
    }
}
