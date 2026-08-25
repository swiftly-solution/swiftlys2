using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuInstance : IMenu
{
    private readonly Dictionary<MenuRegion, List<IMenuComponent>> regions = new() {
        [MenuRegion.Header] = [],
        [MenuRegion.Body] = [],
        [MenuRegion.Footer] = []
    };

    private readonly Dictionary<int, MenuSession> sessions = [];
    private readonly Lock sessionLock = new();
    private readonly Lock componentLock = new();
    private readonly MenuRuntime runtime;

    public MenuInstance(
        string id,
        string owner,
        MenuRuntime runtime,
        IMenuRenderer renderer,
        IMenuKeymap keymap,
        IMenu? parent,
        int itemsPerPage )
    {
        Id = id;
        Owner = owner;
        this.runtime = runtime;
        Renderer = renderer;
        Keymap = keymap;
        Parent = parent;
        ItemsPerPage = Math.Max(1, itemsPerPage);
    }

    public string Id { get; }

    public string Owner { get; }

    public IMenuRenderer Renderer { get; }

    public IMenuKeymap Keymap { get; }

    public IMenu? Parent { get; set; }

    public int ItemsPerPage { get; }

    public object? Tag { get; set; }

    public bool IsDisposed { get; private set; }

    public IReadOnlyList<IMenuSession> Sessions {
        get {
            lock (sessionLock)
            {
                return sessions.Values.Cast<IMenuSession>().ToList();
            }
        }
    }

    public event Action<IMenuSession>? Opened;

    public event Action<IMenuSession>? Closed;

    public event Action<IMenuSession>? FocusChanged;

    public IReadOnlyList<IMenuComponent> GetComponents( MenuRegion region )
    {
        lock (componentLock)
        {
            return regions[region].ToList();
        }
    }

    public void Add( MenuRegion region, IMenuComponent component )
    {
        lock (componentLock)
        {
            regions[region].Add(component);
        }

        InvalidateAll();
    }

    public void Insert( MenuRegion region, int index, IMenuComponent component )
    {
        lock (componentLock)
        {
            var list = regions[region];
            list.Insert(Math.Clamp(index, 0, list.Count), component);
        }

        InvalidateAll();
    }

    public bool Remove( IMenuComponent component )
    {
        var removed = false;

        lock (componentLock)
        {
            foreach (var list in regions.Values)
            {
                removed |= list.Remove(component);
            }
        }

        if (removed)
        {
            InvalidateAll();
        }

        return removed;
    }

    public IMenuSession Open( IPlayer player )
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        var session = new MenuSession(this, player, runtime.Chat);

        lock (sessionLock)
        {
            sessions[player.PlayerID] = session;
        }

        runtime.Attach(session);
        Opened?.Invoke(session);
        return session;
    }

    public void Close( IPlayer player )
    {
        MenuSession? session;

        lock (sessionLock)
        {
            session = sessions.GetValueOrDefault(player.PlayerID);
        }

        if (session is not null)
        {
            runtime.DetachSession(session);
        }
    }

    public void CloseAll()
    {
        List<MenuSession> open;

        lock (sessionLock)
        {
            open = sessions.Values.ToList();
        }

        foreach (var session in open)
        {
            runtime.DetachSession(session);
        }
    }

    public IMenuSession? GetSession( IPlayer player )
    {
        lock (sessionLock)
        {
            return sessions.GetValueOrDefault(player.PlayerID);
        }
    }

    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        CloseAll();

        lock (componentLock)
        {
            foreach (var list in regions.Values)
            {
                list.Clear();
            }
        }
    }

    internal List<IMenuComponent> GetVisible( MenuRegion region, IMenuSession session )
    {
        lock (componentLock)
        {
            return regions[region].Where(component => component.IsVisible(session)).ToList();
        }
    }

    internal List<IMenuComponent> GetFocusables( IMenuSession session )
    {
        lock (componentLock)
        {
            return regions[MenuRegion.Body]
                .Where(component => component.IsFocusable && component.IsVisible(session))
                .ToList();
        }
    }

    internal void NotifyFocusChanged( MenuSession session )
    {
        FocusChanged?.Invoke(session);
    }

    internal void NotifyClosed( MenuSession session )
    {
        lock (sessionLock)
        {
            if (sessions.TryGetValue(session.Player.PlayerID, out var existing) && ReferenceEquals(existing, session))
            {
                _ = sessions.Remove(session.Player.PlayerID);
            }
        }

        Closed?.Invoke(session);
    }

    private void InvalidateAll()
    {
        lock (sessionLock)
        {
            foreach (var session in sessions.Values)
            {
                session.ClampFocus();
                session.Invalidate();
            }
        }
    }
}
