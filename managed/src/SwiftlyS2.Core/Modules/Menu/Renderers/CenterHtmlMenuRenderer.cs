using System.Text;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.Menu.Renderers;

internal sealed class CenterHtmlMenuRenderer : IMenuRenderer
{
    private const string SelectionMarker = "➤ ";
    private const string SelectionPadding = "    ";

    public string Id => MenuRendererIds.CenterHtml;

    public void Render( IMenuRenderContext context )
    {
        var builder = new StringBuilder();
        var lines = new List<string>();

        AppendRegion(context, context.Frame.Header, lines);
        AppendRegion(context, context.Frame.Body, lines);
        AppendRegion(context, context.Frame.Footer, lines);

        for (var index = 0; index < lines.Count; index++)
        {
            if (index > 0)
            {
                _ = builder.Append("<br>");
            }

            _ = builder.Append(lines[index]);
        }

        NativePlayer.SetCenterMenuRender(context.Player.PlayerID, builder.ToString());
    }

    public void Clear( IPlayer player )
    {
        if (!player.IsValid)
        {
            return;
        }

        NativePlayer.ClearCenterMenuRender(player.PlayerID);
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
                for (var index = 0; index < blank.Lines; index++)
                {
                    lines.Add(string.Empty);
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
                return Wrap(text.Text, text.Style);

            case MenuSelectionNode selection:
                return selection.Focused ? SelectionMarker : SelectionPadding;

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

    private static string Wrap( string text, MenuTextStyle style )
    {
        var builder = new StringBuilder();
        _ = builder.Append("<font class='").Append(ToCssClass(style.Size)).Append('\'');

        if (!string.IsNullOrWhiteSpace(style.Color))
        {
            _ = builder.Append(" color='").Append(style.Color).Append('\'');
        }

        _ = builder.Append('>');

        if (style.Bold)
        {
            _ = builder.Append("<b>").Append(text).Append("</b>");
        }
        else
        {
            _ = builder.Append(text);
        }

        _ = builder.Append("</font>");
        return builder.ToString();
    }

    private static string ToCssClass( MenuTextSize size )
    {
        return size switch {
            MenuTextSize.ExtraSmall => "fontSize-xs",
            MenuTextSize.Small => "fontSize-s",
            MenuTextSize.SmallMedium => "fontSize-sm",
            MenuTextSize.Medium => "fontSize-m",
            MenuTextSize.MediumLarge => "fontSize-ml",
            MenuTextSize.Large => "fontSize-l",
            MenuTextSize.ExtraLarge => "fontSize-xl",
            _ => "fontSize-m"
        };
    }
}
