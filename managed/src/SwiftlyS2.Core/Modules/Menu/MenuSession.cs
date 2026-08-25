using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuSession( MenuInstance menu, IPlayer player, MenuChatCapture chat ) : IMenuSession
{
    private readonly Dictionary<(string ComponentId, Type StateType), object> componentState = [];
    private readonly Lock stateLock = new();

    public IPlayer Player { get; } = player;

    public IMenu Menu => menu;

    public MenuInstance Instance => menu;

    public bool IsOpen { get; internal set; } = true;

    public bool IsDirty { get; private set; } = true;

    public int PageOffset { get; internal set; }

    public int FocusedIndex { get; private set; }

    public IMenuComponent? FocusedComponent {
        get {
            var focusables = menu.GetFocusables(this);
            return focusables.Count == 0 ? null : focusables[ClampIndex(FocusedIndex, focusables.Count)];
        }
    }

    public bool MoveFocus( int delta )
    {
        var focusables = menu.GetFocusables(this);

        if (focusables.Count == 0 || delta == 0)
        {
            return false;
        }

        var current = ClampIndex(FocusedIndex, focusables.Count);
        var next = ((current + delta) % focusables.Count + focusables.Count) % focusables.Count;

        if (next == current)
        {
            return false;
        }

        FocusedIndex = next;
        Invalidate();
        menu.NotifyFocusChanged(this);
        return true;
    }

    public bool SetFocus( int index )
    {
        var focusables = menu.GetFocusables(this);

        if (index < 0 || index >= focusables.Count || index == FocusedIndex)
        {
            return false;
        }

        FocusedIndex = index;
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
        IsDirty = true;
    }

    public void Close()
    {
        menu.Close(Player);
    }

    internal void ClearDirty()
    {
        IsDirty = false;
    }

    internal void ClampFocus()
    {
        var focusables = menu.GetFocusables(this);
        var clamped = focusables.Count == 0 ? 0 : ClampIndex(FocusedIndex, focusables.Count);

        if (clamped != FocusedIndex)
        {
            FocusedIndex = clamped;
        }
    }

    private static int ClampIndex( int index, int count ) => Math.Clamp(index, 0, count - 1);
}
