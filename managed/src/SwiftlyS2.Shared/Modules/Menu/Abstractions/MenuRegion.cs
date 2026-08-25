namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Where a component sits in a menu's layout.
/// </summary>
public enum MenuRegion
{
    /// <summary>
    /// Pinned above the body. Never scrolls, never takes focus.
    /// </summary>
    Header,

    /// <summary>
    /// The scrollable, focusable middle of the menu.
    /// </summary>
    Body,

    /// <summary>
    /// Pinned below the body. Never scrolls, never takes focus.
    /// </summary>
    Footer
}
