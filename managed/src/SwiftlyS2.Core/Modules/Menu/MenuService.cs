using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuService : IMenuService, IDisposable
{
    private readonly CoreContext context;
    private readonly MenuRuntime runtime;
    private readonly MenuActionRegistry actionRegistry;
    private readonly MenuRendererRegistry rendererRegistry;
    private readonly MenuKeybindResolver resolver;
    private readonly OwnedMenuActionRegistry ownedActions;
    private readonly OwnedMenuRendererRegistry ownedRenderers;

    private bool disposed;

    public MenuService(
        CoreContext context,
        MenuRuntime runtime,
        MenuActionRegistry actionRegistry,
        MenuRendererRegistry rendererRegistry,
        MenuKeybindResolver resolver )
    {
        this.context = context;
        this.runtime = runtime;
        this.actionRegistry = actionRegistry;
        this.rendererRegistry = rendererRegistry;
        this.resolver = resolver;

        ownedActions = new OwnedMenuActionRegistry(actionRegistry, context.Name);
        ownedRenderers = new OwnedMenuRendererRegistry(rendererRegistry, context.Name);
    }

    public IMenuActionRegistry Actions => ownedActions;

    public IMenuRendererRegistry Renderers => ownedRenderers;

    public IMenuBuilder CreateMenu( string id )
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("A menu id is required.", nameof(id));
        }

        return new MenuBuilder(id, context.Name, runtime, actionRegistry, rendererRegistry, resolver, ownedActions);
    }

    public IMenuSession? GetSession( IPlayer player ) => runtime.GetSession(player.PlayerID);

    public void CloseFor( IPlayer player )
    {
        var session = runtime.GetSession(player.PlayerID);

        if (session is not null && string.Equals(session.Instance.Owner, context.Name, StringComparison.Ordinal))
        {
            runtime.Detach(player.PlayerID);
        }
    }

    public void CloseAll() => runtime.CloseByOwner(context.Name);

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        CloseAll();
        ownedActions.ReleaseAll();
        ownedRenderers.ReleaseAll();
    }
}
