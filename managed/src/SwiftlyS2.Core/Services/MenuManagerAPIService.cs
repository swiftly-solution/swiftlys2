using SwiftlyS2.Shared;
using SwiftlyS2.Core.Menus;

namespace SwiftlyS2.Core.Services;

internal class MenuManagerAPIService : IDisposable
{
    private readonly ISwiftlyCore core;
    private readonly MenuManagerAPI menuManager;

    public MenuManagerAPIService( ISwiftlyCore core, MenuManagerAPI menuManager )
    {
        this.core = core;
        this.menuManager = menuManager;

        menuManager.Core = core;
        core.Event.OnClientKeyStateChanged += menuManager.OnClientKeyStateChanged;
        core.Event.OnClientDisconnected += menuManager.OnClientDisconnected;
        core.Event.OnMapUnload += menuManager.OnMapUnload;
    }

    public void Dispose()
    {
        core.Event.OnClientKeyStateChanged -= menuManager.OnClientKeyStateChanged;
        core.Event.OnClientDisconnected -= menuManager.OnClientDisconnected;
        core.Event.OnMapUnload -= menuManager.OnMapUnload;
    }
}