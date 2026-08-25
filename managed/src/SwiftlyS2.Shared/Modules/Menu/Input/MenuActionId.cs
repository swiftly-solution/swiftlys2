namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Identifies a menu action within a configuration scope.
/// </summary>
/// <param name="Scope">
/// The owning scope. This is either a menu id, or <see cref="MenuActions.CoreScope"/> for the
/// built-in traversal actions every menu inherits.
/// </param>
/// <param name="Name">The action name, unique within its scope.</param>
/// <remarks>
/// The scope maps directly onto a section of the global menu configuration file, and the name
/// onto a key within that section.
/// </remarks>
public readonly record struct MenuActionId( string Scope, string Name )
{
    /// <inheritdoc/>
    public override string ToString() => $"{Scope}:{Name}";
}
