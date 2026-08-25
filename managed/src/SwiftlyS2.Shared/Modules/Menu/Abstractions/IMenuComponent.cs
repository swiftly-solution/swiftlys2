namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// A reusable unit a menu is built from.
/// </summary>
/// <remarks>
/// A component owns exactly two things: how it draws, and how it reacts to actions. It owns no
/// per-player state, which is what makes a single instance safe to place in several menus and to
/// show to several players at once. Anything that varies per player belongs in
/// <see cref="IMenuSession"/> state.
/// <para>
/// Prefer deriving from <see cref="MenuComponentBase"/> rather than implementing this directly.
/// </para>
/// </remarks>
public interface IMenuComponent
{
    /// <summary>
    /// A stable id for this component, unique within its menu.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Whether the selection can land on this component.
    /// </summary>
    public bool IsFocusable { get; }

    /// <summary>
    /// Whether activating this component plays the menu's selection sound.
    /// </summary>
    public bool PlaySound => true;

    /// <summary>
    /// How many lines this component draws.
    /// </summary>
    /// <remarks>
    /// Used to decide how much of the body fits on one page, without rendering components that will
    /// not be shown. It must match what <see cref="Render"/> produces: a <see cref="MenuBlankNode"/>
    /// costs its <see cref="MenuBlankNode.Lines"/>, a <see cref="MenuStackNode"/> costs the sum of
    /// its children, and everything else costs one. A component that returns a stack has to override
    /// this, otherwise it is budgeted as a single line and the page overflows.
    /// </remarks>
    public int LineCount => 1;

    /// <summary>
    /// Whether this component is shown to a player.
    /// </summary>
    /// <param name="session">The session asking.</param>
    /// <returns><see langword="true"/> when the component should be drawn.</returns>
    public bool IsVisible( IMenuSession session );

    /// <summary>
    /// Whether this component can be interacted with by a player.
    /// </summary>
    /// <param name="session">The session asking.</param>
    /// <returns><see langword="true"/> when the component accepts actions.</returns>
    public bool IsEnabled( IMenuSession session );

    /// <summary>
    /// Produces the hint shown while this component holds the selection.
    /// </summary>
    /// <param name="session">The session asking.</param>
    /// <returns>The hint text, or null to show nothing.</returns>
    /// <remarks>
    /// Use this for a per-entry explanation, or to advertise keys the component takes over through
    /// <see cref="HandleActionAsync"/>.
    /// </remarks>
    public string? GetHint( IMenuSession session ) => null;

    /// <summary>
    /// Whether this component needs a new frame even though nothing invalidated the session.
    /// </summary>
    /// <param name="session">The session asking.</param>
    /// <param name="now">The current time.</param>
    /// <returns><see langword="true"/> to redraw on this tick.</returns>
    /// <remarks>
    /// Asked once per tick for every visible component of an otherwise clean session, so keep the
    /// check cheap. Animated text and live values are what this exists for.
    /// </remarks>
    public bool NeedsRedraw( IMenuSession session, DateTime now ) => false;

    /// <summary>
    /// Produces this component's renderer-agnostic output.
    /// </summary>
    /// <param name="context">The player and focus state to draw for.</param>
    /// <returns>The nodes describing this component.</returns>
    public MenuNode Render( IMenuComponentRenderContext context );

    /// <summary>
    /// Offers an action to this component before the menu handles it.
    /// </summary>
    /// <param name="context">The action being dispatched.</param>
    /// <returns>
    /// <see langword="true"/> to consume the action and stop the menu's own handling from running.
    /// </returns>
    /// <remarks>
    /// Only the focused component is offered actions. Returning <see langword="true"/> is how a
    /// component takes over a key that would otherwise navigate or close the menu.
    /// </remarks>
    public ValueTask<bool> HandleActionAsync( MenuActionContext context );
}
