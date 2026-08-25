namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The built-in traversal actions that every menu inherits automatically.
/// </summary>
/// <remarks>
/// These live in the reserved <see cref="CoreScope"/> scope. A menu may rebind them by declaring
/// an action with the same <see cref="MenuActionId.Name"/> in its own scope.
/// </remarks>
public static class MenuActions
{
    /// <summary>
    /// The reserved configuration scope holding the automatic traversal binds.
    /// </summary>
    public const string CoreScope = "core.menu";

    /// <summary>
    /// Moves the selection to the previous focusable component.
    /// </summary>
    public static readonly MenuActionId NavigateUp = new(CoreScope, "NavigateUp");

    /// <summary>
    /// Moves the selection to the next focusable component.
    /// </summary>
    public static readonly MenuActionId NavigateDown = new(CoreScope, "NavigateDown");

    /// <summary>
    /// Steps the focused component's value towards its start.
    /// </summary>
    /// <remarks>
    /// Has no effect of its own. Only components that take it over in
    /// <see cref="IMenuComponent.HandleActionAsync"/> — sliders and selectors — react to it.
    /// </remarks>
    public static readonly MenuActionId NavigateLeft = new(CoreScope, "NavigateLeft");

    /// <summary>
    /// Steps the focused component's value towards its end.
    /// </summary>
    /// <remarks>
    /// Has no effect of its own, exactly as <see cref="NavigateLeft"/>.
    /// </remarks>
    public static readonly MenuActionId NavigateRight = new(CoreScope, "NavigateRight");

    /// <summary>
    /// Activates the focused component.
    /// </summary>
    public static readonly MenuActionId Select = new(CoreScope, "Select");

    /// <summary>
    /// Closes the menu, returning to its parent when it has one.
    /// </summary>
    public static readonly MenuActionId Close = new(CoreScope, "Close");
}
