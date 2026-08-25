namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The resolved set of actions and keys for one menu.
/// </summary>
/// <remarks>
/// A keymap combines the inherited <see cref="MenuActions.CoreScope"/> traversal actions with the
/// menu's own actions, then resolves each one through the registered keybind sources.
/// </remarks>
public interface IMenuKeymap
{
    /// <summary>
    /// Every action available in this menu, ordered by <see cref="MenuActionDescriptor.Order"/>.
    /// </summary>
    public IReadOnlyList<MenuActionDescriptor> Actions { get; }

    /// <summary>
    /// Gets the key currently bound to an action.
    /// </summary>
    /// <param name="id">The action to resolve.</param>
    /// <returns>The bound key, or <see cref="MenuKey.None"/> when the action is unknown or unbound.</returns>
    public MenuKey GetKey( MenuActionId id );

    /// <summary>
    /// Finds the action bound to a key.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    /// <param name="action">The matching action, when one is bound.</param>
    /// <returns><see langword="true"/> when an action is bound to the key.</returns>
    /// <remarks>
    /// Actions declared in the menu's own scope take precedence over inherited core actions bound
    /// to the same key.
    /// </remarks>
    public bool TryResolve( MenuKey key, out MenuActionId action );
}
