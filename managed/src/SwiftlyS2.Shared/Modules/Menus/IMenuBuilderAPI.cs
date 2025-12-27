namespace SwiftlyS2.Shared.Menus;

/// <summary>
/// Provides a fluent builder interface for creating and configuring menus.
/// All methods support chaining for convenient menu construction.
/// </summary>
public interface IMenuBuilderAPI
{
    /// <summary>
    /// Gets the design interface for this menu.
    /// </summary>
    public IMenuDesignAPI Design { get; }

    /// <summary>
    /// Binds this menu to a parent menu, creating a hierarchical navigation structure.
    /// </summary>
    /// <param name="parent">The parent menu.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI BindToParent( IMenuAPI parent );

    /// <summary>
    /// Adds a menu option to the menu.
    /// </summary>
    /// <param name="option">The menu option to add.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI AddOption( IMenuOption option );

    /// <summary>
    /// Enables sound effects for menu interactions.
    /// </summary>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI EnableSound();

    /// <summary>
    /// Disables sound effects for menu interactions.
    /// </summary>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI DisableSound();

    /// <summary>
    /// Enables the exit button for this menu.
    /// </summary>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI EnableExit();

    /// <summary>
    /// Disables the exit button for this menu.
    /// </summary>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI DisableExit();

    /// <summary>
    /// Controls whether player movement is frozen while the menu is open.
    /// </summary>
    /// <param name="frozen">True to freeze player movement, false to allow movement. Default is false.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI SetPlayerFrozen( bool frozen = false );

    /// <summary>
    /// Sets the automatic close delay for the menu.
    /// </summary>
    /// <param name="seconds">Time in seconds before the menu automatically closes. Set to 0 to disable auto-close. Default is 0.</param>
    /// <returns>This builder for method chaining.</returns>
    public IMenuBuilderAPI SetAutoCloseDelay( float seconds = 0f );

    /// <summary>
    /// Overrides the default key binding for selecting menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// This overrides the default select button behavior.
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.Mouse1 | KeyBind.E</c> allows either Mouse1 or E to select options.
    /// </remarks>
    public IMenuBuilderAPI SetSelectButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the default key binding for moving forward through menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// This overrides the default move forward button behavior.
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.W | KeyBind.Mouse1</c> allows either W or Mouse1 to move forward.
    /// </remarks>
    public IMenuBuilderAPI SetMoveForwardButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the default key binding for moving backward through menu options.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// This overrides the default move backward button behavior.
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.S | KeyBind.Mouse2</c> allows either S or Mouse2 to move backward.
    /// </remarks>
    public IMenuBuilderAPI SetMoveBackwardButton( KeyBind keyBind );

    /// <summary>
    /// Overrides the default key binding for closing the menu.
    /// </summary>
    /// <param name="keyBind">The key binding to use.</param>
    /// <returns>This builder for method chaining.</returns>
    /// <remarks>
    /// This overrides the default exit button behavior.
    /// Supports multiple key bindings using the bitwise OR operator.
    /// Example: <c>KeyBind.Esc | KeyBind.A</c> allows either Esc or A to close the menu.
    /// </remarks>
    public IMenuBuilderAPI SetExitButton( KeyBind keyBind );

    /// <summary>
    /// Builds the menu and returns the final menu instance.
    /// </summary>
    /// <returns>The built menu instance.</returns>
    public IMenuAPI Build();
}