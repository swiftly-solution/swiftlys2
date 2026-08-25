using System.Text;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menu.Renderers;

internal sealed class ChatMenuRenderer : IMenuRenderer
{
    public int BlankLinesBefore { get; set; } = 3;

    public string Id => MenuRendererIds.Chat;

    public void Render( IMenuRenderContext context )
    {
        var player = context.Player;

        for (var i = 0; i < BlankLinesBefore; i++)
        {
            player.SendChat(" ");
        }

        var lines = new List<string>();

        AppendRegion(context, context.Frame.Header, lines);
        AppendRegion(context, context.Frame.Body, lines);
        AppendRegion(context, context.Frame.Footer, lines);

        foreach (var line in lines)
        {
            player.SendChat(line);
        }
    }

    public void Clear( IPlayer player )
    {
        // Chat has no persistent render target to clear - the lines already sent just scroll away.
    }

    private void AppendRegion( IMenuRenderContext context, IReadOnlyList<MenuNode> nodes, List<string> lines )
    {
        foreach (var node in nodes)
        {
            AppendNode(context, node, lines);
        }
    }

    private void AppendNode( IMenuRenderContext context, MenuNode node, List<string> lines )
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
                    AppendNode(context, child, lines);
                }

                break;

            default:
                var inline = RenderInline(context, node);

                if (inline is not null)
                {
                    lines.Add(inline);
                }

                break;
        }
    }

    private string? RenderInline( IMenuRenderContext context, MenuNode node )
    {
        switch (node)
        {
            case MenuTextNode text:
                return text.Text;

            case MenuSelectionNode selection:
                return selection.Focused ? "> " : "  ";

            case MenuRawNode raw:
                return string.Equals(raw.RendererId, Id, StringComparison.OrdinalIgnoreCase) ? raw.Payload : null;

            case MenuLineNode line:
                var builder = new StringBuilder();

                foreach (var child in line.Children)
                {
                    var rendered = RenderInline(context, child);

                    if (rendered is not null)
                    {
                        _ = builder.Append(rendered);
                    }
                }

                return builder.ToString();

            case MenuBlankNode:
            case MenuStackNode:
                return null;

            default:
                context.ReportUnsupported(node);
                return null;
        }
    }
}
