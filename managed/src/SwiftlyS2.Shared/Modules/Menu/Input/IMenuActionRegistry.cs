namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The dynamic catalogue of bindable menu actions.
/// </summary>
/// <remarks>
/// Registrations made through a plugin's own menu service are released automatically when that
/// plugin unloads, so a plugin never has to unregister its actions by hand.
/// </remarks>
public interface IMenuActionRegistry
{
    /// <summary>
    /// Declares an action, replacing any previous declaration with the same id.
    /// </summary>
    /// <param name="descriptor">The action to declare.</param>
    /// <returns>A handle that removes the declaration when disposed.</returns>
    public IDisposable Register( MenuActionDescriptor descriptor );

    /// <summary>
    /// Removes an action declaration.
    /// </summary>
    /// <param name="id">The action to remove.</param>
    /// <returns><see langword="true"/> when an action was removed.</returns>
    public bool Unregister( MenuActionId id );

    /// <summary>
    /// Looks up a declared action.
    /// </summary>
    /// <param name="id">The action to find.</param>
    /// <param name="descriptor">The declaration, when found.</param>
    /// <returns><see langword="true"/> when the action is declared.</returns>
    public bool TryGet( MenuActionId id, out MenuActionDescriptor descriptor );

    /// <summary>
    /// Lists every action declared in a scope.
    /// </summary>
    /// <param name="scope">The scope to enumerate, typically a menu id.</param>
    /// <returns>The declarations, ordered by <see cref="MenuActionDescriptor.Order"/>.</returns>
    public IReadOnlyList<MenuActionDescriptor> GetScope( string scope );
}
