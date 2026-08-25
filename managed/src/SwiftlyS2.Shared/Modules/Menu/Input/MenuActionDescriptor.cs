namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Declares a bindable menu action along with the key it uses when nothing overrides it.
/// </summary>
/// <remarks>
/// Descriptors are the only place an action is defined. Nothing in the framework hardcodes a key,
/// which is what lets plugins add and remove actions at runtime.
/// </remarks>
public sealed record MenuActionDescriptor
{
    /// <summary>
    /// The identity of this action.
    /// </summary>
    public required MenuActionId Id { get; init; }

    /// <summary>
    /// The key used when neither the global nor the plugin configuration binds this action.
    /// </summary>
    public required MenuKey DefaultKey { get; init; }

    /// <summary>
    /// The label shown for this action in a menu footer. Falls back to <see cref="MenuActionId.Name"/> when null.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Whether renderers should advertise this action in the menu footer.
    /// </summary>
    public bool ShowInFooter { get; init; } = true;

    /// <summary>
    /// Relative ordering of this action within a footer. Lower sorts first.
    /// </summary>
    public int Order { get; init; }
}
