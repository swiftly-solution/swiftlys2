using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// One player's live view of an open menu.
/// </summary>
/// <remarks>
/// Every piece of state that varies per player lives here rather than on the menu or its
/// components, which is what allows one component instance to be shared across menus and players.
/// </remarks>
public interface IMenuSession
{
    /// <summary>
    /// The player viewing the menu.
    /// </summary>
    public IPlayer Player { get; }

    /// <summary>
    /// The menu being viewed.
    /// </summary>
    public IMenu Menu { get; }

    /// <summary>
    /// Whether this session is still open.
    /// </summary>
    public bool IsOpen { get; }

    /// <summary>
    /// The index of the focused component within the visible focusable body components.
    /// </summary>
    public int FocusedIndex { get; }

    /// <summary>
    /// The focused component, or null when the menu has nothing focusable.
    /// </summary>
    public IMenuComponent? FocusedComponent { get; }

    /// <summary>
    /// Moves the selection by a number of entries, wrapping at both ends.
    /// </summary>
    /// <param name="delta">How far to move. Negative moves towards the top.</param>
    /// <returns><see langword="true"/> when the selection changed.</returns>
    public bool MoveFocus( int delta );

    /// <summary>
    /// Moves the selection to a specific entry.
    /// </summary>
    /// <param name="index">The index among visible focusable body components.</param>
    /// <returns><see langword="true"/> when the index was valid and the selection changed.</returns>
    public bool SetFocus( int index );

    /// <summary>
    /// Gets this player's state for a component, creating it on first use.
    /// </summary>
    /// <typeparam name="TState">The state type. Must be default constructible.</typeparam>
    /// <param name="component">The component the state belongs to.</param>
    /// <returns>The per-player state instance.</returns>
    public TState GetState<TState>( IMenuComponent component ) where TState : class, new();

    /// <summary>
    /// Replaces this player's state for a component.
    /// </summary>
    /// <typeparam name="TState">The state type.</typeparam>
    /// <param name="component">The component the state belongs to.</param>
    /// <param name="state">The state to store.</param>
    public void SetState<TState>( IMenuComponent component, TState state ) where TState : class;

    /// <summary>
    /// Routes this player's next chat messages to a handler instead of the chat.
    /// </summary>
    /// <param name="onMessage">
    /// Receives each message. Returning <see langword="true"/> swallows it so it never reaches the
    /// chat; returning <see langword="false"/> lets it through untouched.
    /// </param>
    /// <returns>A handle that stops the capture when disposed.</returns>
    /// <remarks>
    /// Only one capture is active per session; starting a second one replaces the first. The
    /// capture is dropped automatically when the menu closes or the player disconnects, so a
    /// component only needs to dispose the handle when it finishes early.
    /// </remarks>
    public IDisposable CaptureChat( Func<string, bool> onMessage );

    /// <summary>
    /// Marks this session as needing a redraw on the next tick.
    /// </summary>
    /// <remarks>
    /// Call this after changing anything that affects what the player sees. Frames are only sent
    /// when a session is invalidated, so forgetting this leaves the display stale.
    /// </remarks>
    public void Invalidate();

    /// <summary>
    /// Closes this session, returning the player to the parent menu when there is one.
    /// </summary>
    public void Close();
}
