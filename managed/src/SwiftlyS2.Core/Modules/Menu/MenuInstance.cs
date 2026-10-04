using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuInstance : IMenu
{
    private volatile IMenuComponent[][] regions = [[], [], []];

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
        MenuInputMethod inputMethod,
        IMenu? parent,
        int itemsPerPage )
    {
        Id = id;
        Owner = owner;
        this.runtime = runtime;
        Renderer = renderer;
        Keymap = keymap;
        InputMethod = inputMethod;
        Parent = parent;
        ItemsPerPage = Math.Max(1, itemsPerPage);
    }

    public string Id { get; }

    public string Owner { get; }

    public IMenuRenderer Renderer { get; }

    public IMenuKeymap Keymap { get; }

    public MenuInputMethod InputMethod { get; }

    public IMenu? Parent { get; set; }

    public int ItemsPerPage { get; }

    public object? Tag { get; set; }

    public bool IsDisposed { get; private set; }

    internal MenuHintCache? ButtonHints { get; set; }

    internal MenuNode? ChatHints { get; set; }

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

    public IReadOnlyList<IMenuComponent> GetComponents( MenuRegion region ) => [.. regions[(int)region]];

    public void Add( MenuRegion region, IMenuComponent component )
    {
        Replace(region, current => [.. current, component]);
        InvalidateAll();
    }

    public void Insert( MenuRegion region, int index, IMenuComponent component )
    {
        Replace(region, current => {
            var list = current.ToList();
            list.Insert(Math.Clamp(index, 0, list.Count), component);
            return [.. list];
        });

        InvalidateAll();
    }

    public bool Remove( IMenuComponent component )
    {
        var removed = false;

        lock (componentLock)
        {
            var next = regions.ToArray();

            for (var region = 0; region < next.Length; region++)
            {
                var position = Array.IndexOf(next[region], component);

                if (position >= 0)
                {
                    next[region] = [.. next[region][..position], .. next[region][(position + 1)..]];
                    removed = true;
                }
            }

            if (removed)
            {
                regions = next;
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
            regions = [[], [], []];
        }
    }

    internal IMenuComponent[] Snapshot( MenuRegion region ) => regions[(int)region];

    internal List<IMenuComponent> GetVisible( MenuRegion region, IMenuSession session )
    {
        var source = Snapshot(region);
        var visible = new List<IMenuComponent>(source.Length);

        foreach (var component in source)
        {
            if (component.IsVisible(session))
            {
                visible.Add(component);
            }
        }

        return visible;
    }

    internal List<IMenuComponent> GetFocusables( IMenuSession session )
    {
        var source = Snapshot(MenuRegion.Body);
        var focusables = new List<IMenuComponent>(source.Length);

        foreach (var component in source)
        {
            if (component.IsFocusable && component.IsVisible(session))
            {
                focusables.Add(component);
            }
        }

        return focusables;
    }

    internal int CountFocusables( IMenuSession session )
    {
        var count = 0;

        foreach (var component in Snapshot(MenuRegion.Body))
        {
            if (component.IsFocusable && component.IsVisible(session))
            {
                count++;
            }
        }

        return count;
    }

    internal IMenuComponent? FocusableAt( IMenuSession session, int index )
    {
        var position = 0;

        foreach (var component in Snapshot(MenuRegion.Body))
        {
            if (component.IsFocusable && component.IsVisible(session) && position++ == index)
            {
                return component;
            }
        }

        return null;
    }

    internal int IndexOfFocusable( IMenuSession session, IMenuComponent target )
    {
        var position = 0;

        foreach (var component in Snapshot(MenuRegion.Body))
        {
            if (component.IsFocusable && component.IsVisible(session))
            {
                if (ReferenceEquals(component, target))
                {
                    return position;
                }

                position++;
            }
        }

        return -1;
    }

    private void Replace( MenuRegion region, Func<IMenuComponent[], IMenuComponent[]> change )
    {
        lock (componentLock)
        {
            var next = regions.ToArray();
            next[(int)region] = change(next[(int)region]);
            regions = next;
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
