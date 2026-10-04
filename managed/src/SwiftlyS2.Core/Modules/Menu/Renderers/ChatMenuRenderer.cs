using System.Text;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu.Renderers;

internal sealed class ChatMenuRenderer : IMenuRenderer
{
    private const int MaxRetainedCapacity = 16 * 1024;

    [ThreadStatic]
    private static StringBuilder? scratch;

    public int BlankLinesBefore { get; set; } = 3;

    public string Id => MenuRendererIds.Chat;

    public void Render( IMenuRenderContext context )
    {
        var player = context.Player;

        for (var i = 0; i < BlankLinesBefore; i++)
        {
            player.SendChat(" ");
        }

        foreach (var line in BuildLines(context))
        {
            player.SendChat(line);
        }
    }

    public void Clear( IPlayer player )
    {
        // Chat has no persistent render target to clear - the lines already sent just scroll away.
    }

    internal List<string> BuildLines( IMenuRenderContext context )
    {
        var frame = context.Frame;
        var lines = new List<string>(frame.Header.Count + frame.Body.Count + frame.Footer.Count);
        var builder = scratch ??= new StringBuilder(256);

        AppendRegion(context, frame.Header, lines, builder);
        AppendRegion(context, frame.Body, lines, builder);
        AppendRegion(context, frame.Footer, lines, builder);

        if (builder.Capacity > MaxRetainedCapacity)
        {
            scratch = null;
        }

        return lines;
    }

    private void AppendRegion( IMenuRenderContext context, IReadOnlyList<MenuNode> nodes, List<string> lines, StringBuilder builder )
    {
        foreach (var node in nodes)
        {
            AppendNode(context, node, lines, builder);
        }
    }

    private void AppendNode( IMenuRenderContext context, MenuNode node, List<string> lines, StringBuilder builder )
    {
        switch (node)
        {
            case MenuBlankNode blank:
                for (var i = 0; i < blank.Lines; i++)
                {
                    lines.Add(" ");
                }

                break;

            case MenuStackNode stack:
                foreach (var child in stack.Children)
                {
                    AppendNode(context, child, lines, builder);
                }

                break;

            case MenuTextNode text:
                lines.Add(text.Text);
                break;

            default:
                _ = builder.Clear();

                if (AppendInline(context, node, builder))
                {
                    lines.Add(builder.ToString());
                }

                break;
        }
    }

    private bool AppendInline( IMenuRenderContext context, MenuNode node, StringBuilder builder )
    {
        switch (node)
        {
            case MenuTextNode text:
                _ = builder.Append(text.Text);
                return true;

            case MenuSelectionNode selection:
                if (selection.Number > 0)
                {
                    _ = builder.Append(selection.Number).Append(". ");
                    return true;
                }

                _ = builder.Append(selection.Focused ? "> " : "  ");
                return true;

            case MenuRawNode raw:
                if (!string.Equals(raw.RendererId, Id, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                _ = builder.Append(raw.Payload);
                return true;

            case MenuLineNode line:
                foreach (var child in line.Children)
                {
                    _ = AppendInline(context, child, builder);
                }

                return true;

            case MenuBlankNode:
            case MenuStackNode:
                return false;

            default:
                context.ReportUnsupported(node);
                return false;
        }
    }
}
