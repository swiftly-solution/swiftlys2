namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a simple text-only menu option without interactive behavior.
/// </summary>
public sealed class TextMenuOption : MenuOptionBase
{
    /// <summary>
    /// Creates an instance of <see cref="TextMenuOption"/> with dynamic text updating capabilities.
    /// </summary>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public TextMenuOption(
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = false;
    }

    /// <summary>
    /// Creates an instance of <see cref="TextMenuOption"/> with dynamic text updating capabilities.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public TextMenuOption(
        string text,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }
}