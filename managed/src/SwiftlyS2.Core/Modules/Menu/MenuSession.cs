using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuSession( MenuInstance menu, IPlayer player, MenuChatCapture chat ) : IMenuSession
{
    private readonly Dictionary<(string ComponentId, Type StateType), object> componentState = [];
    private readonly Lock stateLock = new();

    private bool isOpen = true;
    private bool isDirty = true;
    private int pageOffset;
    private int focusedIndex;
    private IReadOnlyList<IMenuComponent> pageSelectables = [];

    public IPlayer Player { get; } = player;

    public IMenu Menu => menu;

    public MenuInstance Instance => menu;

    public bool IsOpen {
        get { lock (stateLock) { return isOpen; } }
        internal set { lock (stateLock) { isOpen = value; } }
    }

    public bool IsDirty {
        get { lock (stateLock) { return isDirty; } }
    }

    public int PageOffset {
        get { lock (stateLock) { return pageOffset; } }
        internal set { lock (stateLock) { pageOffset = value; } }
    }

    public int FocusedIndex {
        get { lock (stateLock) { return focusedIndex; } }
        private set { lock (stateLock) { focusedIndex = value; } }
    }

    public IReadOnlyList<IMenuComponent> PageSelectables {
        get { lock (stateLock) { return pageSelectables; } }
        internal set { lock (stateLock) { pageSelectables = value; } }
    }

    public IMenuComponent? FocusedComponent {
        get {
            var focusables = menu.GetFocusables(this);

            if (focusables.Count == 0)
            {
                return null;
            }

            lock (stateLock)
            {
                return focusables[ClampIndex(focusedIndex, focusables.Count)];
            }
        }
    }

    public bool MoveFocus( int delta )
    {
        var focusables = menu.GetFocusables(this);

        if (focusables.Count == 0 || delta == 0)
        {
            return false;
        }

        lock (stateLock)
        {
            var current = ClampIndex(focusedIndex, focusables.Count);
            var next = ((current + delta) % focusables.Count + focusables.Count) % focusables.Count;

            if (next == current)
            {
                return false;
            }

            focusedIndex = next;
        }

        Invalidate();
        menu.NotifyFocusChanged(this);
        return true;
    }

    public bool SetFocus( int index )
    {
        var focusables = menu.GetFocusables(this);

        lock (stateLock)
        {
            if (index < 0 || index >= focusables.Count || index == focusedIndex)
            {
                return false;
            }

            focusedIndex = index;
        }

        Invalidate();
        menu.NotifyFocusChanged(this);
        return true;
    }

    public TState GetState<TState>( IMenuComponent component ) where TState : class, new()
    {
        lock (stateLock)
        {
            var key = (component.Id, typeof(TState));

            if (componentState.TryGetValue(key, out var existing) && existing is TState typed)
            {
                return typed;
            }

            var created = new TState();
            componentState[key] = created;
            return created;
        }
    }

    public void SetState<TState>( IMenuComponent component, TState state ) where TState : class
    {
        lock (stateLock)
        {
            componentState[(component.Id, typeof(TState))] = state;
        }
    }

    public IDisposable CaptureChat( Func<string, bool> onMessage )
    {
        return chat.Capture(Player.PlayerID, onMessage);
    }

    public void Invalidate()
    {
        lock (stateLock)
        {
            isDirty = true;
        }
    }

    public void Close()
    {
        menu.Close(Player);
    }

    internal void ClearDirty()
    {
        lock (stateLock)
        {
            isDirty = false;
        }
    }

    internal void ClampFocus()
    {
        var focusables = menu.GetFocusables(this);

        lock (stateLock)
        {
            var clamped = focusables.Count == 0 ? 0 : ClampIndex(focusedIndex, focusables.Count);

            if (clamped != focusedIndex)
            {
                focusedIndex = clamped;
            }
        }
    }

    private static int ClampIndex( int index, int count ) => Math.Clamp(index, 0, count - 1);
}
