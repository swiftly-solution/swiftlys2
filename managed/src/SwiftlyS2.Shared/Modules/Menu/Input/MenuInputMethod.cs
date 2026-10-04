namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// How a player interacts with an open menu.
/// </summary>
public enum MenuInputMethod
{
    /// <summary>
    /// Keyboard buttons bound to menu actions, as resolved by the menu's <see cref="IMenuKeymap"/>.
    /// </summary>
    Buttons,

    /// <summary>
    /// Chat commands. Typing <c>!1</c>, <c>!2</c> and so on selects the entry with that number on
    /// the current page, and <c>!0</c> closes the menu.
    /// </summary>
    /// <remarks>
    /// Keyboard buttons are ignored while this method is active. Matching messages are swallowed
    /// and never reach the chat.
    /// </remarks>
    Chat
}
