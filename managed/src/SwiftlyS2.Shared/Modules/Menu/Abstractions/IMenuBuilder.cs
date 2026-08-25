namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Fluent construction of a menu.
/// </summary>
public interface IMenuBuilder
{
    /// <summary>
    /// Chooses the renderer this menu draws through.
    /// </summary>
    /// <param name="rendererId">The registered renderer id.</param>
    /// <returns>This builder.</returns>
    /// <remarks>
    /// Defaults to <see cref="MenuRendererIds.CenterHtml"/>. Building throws when the id is not
    /// registered.
    /// </remarks>
    public IMenuBuilder WithRenderer( string rendererId );

    /// <summary>
    /// Sets the menu returned to when this one closes.
    /// </summary>
    /// <param name="parent">The parent menu.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder WithParent( IMenu parent );

    /// <summary>
    /// Sets the largest number of body entries shown at once.
    /// </summary>
    /// <param name="itemsPerPage">The page size. Must be at least one.</param>
    /// <returns>This builder.</returns>
    /// <remarks>
    /// An upper bound, not a guarantee. The server-wide line budget can cut the page down further,
    /// so entries that draw more than one line — spacers, multi-line components — leave room for
    /// fewer neighbours. This is what keeps the last entries of a long menu reachable instead of
    /// pushing them off screen.
    /// </remarks>
    public IMenuBuilder WithItemsPerPage( int itemsPerPage );

    /// <summary>
    /// Declares an action available in this menu.
    /// </summary>
    /// <param name="descriptor">The action to declare.</param>
    /// <returns>This builder.</returns>
    /// <remarks>
    /// Declaring an action whose name matches a built-in traversal action rebinds that action for
    /// this menu only.
    /// </remarks>
    public IMenuBuilder WithAction( MenuActionDescriptor descriptor );

    /// <summary>
    /// Declares an action available in this menu, in the menu's own scope.
    /// </summary>
    /// <param name="name">The action name.</param>
    /// <param name="defaultKey">The key to use when nothing overrides it.</param>
    /// <param name="label">The footer label, or null to use the name.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder WithAction( string name, MenuKey defaultKey, string? label = null );

    /// <summary>
    /// Adds a source of key overrides for this menu.
    /// </summary>
    /// <param name="source">The source to consult.</param>
    /// <returns>This builder.</returns>
    /// <remarks>
    /// Use this to bind actions from a plugin's own configuration. The server-wide configuration
    /// still wins, since it registers at a higher priority.
    /// </remarks>
    public IMenuBuilder WithKeybindSource( IMenuKeybindSource source );

    /// <summary>
    /// Adds a title line to the header.
    /// </summary>
    /// <param name="title">The title text.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder WithTitle( string title );

    /// <summary>
    /// Appends a component to the header.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder AddHeader( IMenuComponent component );

    /// <summary>
    /// Appends a component to the body.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder Add( IMenuComponent component );

    /// <summary>
    /// Appends a component to the footer.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <returns>This builder.</returns>
    public IMenuBuilder AddFooter( IMenuComponent component );

    /// <summary>
    /// Builds the menu.
    /// </summary>
    /// <returns>The built menu.</returns>
    public IMenu Build();
}
