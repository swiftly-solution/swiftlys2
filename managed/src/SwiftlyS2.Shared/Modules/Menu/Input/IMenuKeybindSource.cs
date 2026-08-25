namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Supplies key overrides for menu actions.
/// </summary>
/// <remarks>
/// Sources are consulted in descending <see cref="Priority"/> order and the first one that answers
/// wins, so layering configuration is a matter of registering another source rather than merging
/// values. Implement this to bind actions from any store.
/// </remarks>
public interface IMenuKeybindSource
{
    /// <summary>
    /// The resolution priority. Higher values are consulted first.
    /// </summary>
    /// <seealso cref="MenuKeybindPriority"/>
    public int Priority { get; }

    /// <summary>
    /// Attempts to resolve the key bound to an action.
    /// </summary>
    /// <param name="id">The action to resolve.</param>
    /// <param name="key">The bound key, when this source binds the action.</param>
    /// <returns><see langword="true"/> when this source binds the action.</returns>
    public bool TryGetKey( MenuActionId id, out MenuKey key );
}

/// <summary>
/// The priorities used by the built-in keybind sources.
/// </summary>
public static class MenuKeybindPriority
{
    /// <summary>
    /// The default key declared on the action itself. Lowest priority.
    /// </summary>
    public const int CodeDefault = 0;

    /// <summary>
    /// A binding coming from the owning plugin's own configuration.
    /// </summary>
    public const int Plugin = 50;

    /// <summary>
    /// A binding coming from the server-wide menu configuration file. Highest priority.
    /// </summary>
    public const int Global = 100;
}
