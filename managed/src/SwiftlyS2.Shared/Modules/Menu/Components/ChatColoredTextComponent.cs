using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A line coloured with one of <c>Helper.ChatColors</c>' tags rather than a hex
/// <see cref="MenuTextStyle.Color"/>. Can be scrolled to but never does anything when activated.
/// </summary>
/// <remarks>
/// On the <see cref="MenuRendererIds.Chat"/> renderer this prints with the actual chat colour code.
/// On any other renderer, such as the default <see cref="MenuRendererIds.CenterHtml"/>, <see cref="Render"/>
/// maps <see cref="ChatColor"/> to its closest hex equivalent instead of dropping colour entirely.
/// </remarks>
public class ChatColoredTextComponent : MenuComponentBase
{
    /// <summary>
    /// Creates a chat-coloured text line.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="chatColor">One of <c>Helper.ChatColors</c>' tags, or null for <see cref="Helper.ChatColors.Default"/>.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ChatColoredTextComponent( string text, string? chatColor = null, string? id = null ) : base(id)
    {
        Text = text;
        ChatColor = chatColor ?? Helper.ChatColors.Default;
    }

    /// <summary>
    /// The text to display.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// One of <c>Helper.ChatColors</c>' tags, e.g. <see cref="Helper.ChatColors.Green"/>.
    /// </summary>
    public string ChatColor { get; set; }

    /// <inheritdoc/>
    /// <remarks>
    /// A label can hold the selection like any other body entry; it just never does anything when
    /// activated.
    /// </remarks>
    public override bool IsFocusable => true;

    /// <inheritdoc/>
    /// <remarks>
    /// The generic fallback used by any renderer other than <see cref="MenuRendererIds.Chat"/>.
    /// </remarks>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        var style = MenuTextStyle.Default.WithSize(MenuTextSize.SmallMedium);

        if (ChatColorPalette.TryGetHex(ChatColor, out var hex))
        {
            style = style.WithColor(hex);
        }

        return new MenuTextNode(Text, style);
    }
}
