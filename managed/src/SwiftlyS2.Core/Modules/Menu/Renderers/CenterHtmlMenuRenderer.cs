using System.Text;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.Menu.Renderers;

internal sealed class CenterHtmlMenuRenderer : IMenuRenderer
{
    private const string SelectionMarker = "➤ ";
    private const string SelectionPadding = "    ";
    private const int MaxRetainedCapacity = 64 * 1024;

    [ThreadStatic]
    private static StringBuilder? scratch;

    public string Id => MenuRendererIds.CenterHtml;

    public void Render( IMenuRenderContext context )
    {
        NativePlayer.SetCenterMenuRender(context.Player.PlayerID, Build(context));
    }

    public void Clear( IPlayer player )
    {
        if (!player.IsValid)
        {
            return;
        }

        NativePlayer.ClearCenterMenuRender(player.PlayerID);
    }

    internal string Build( IMenuRenderContext context )
    {
        var builder = scratch ??= new StringBuilder(2048);
        _ = builder.Clear();

        var lines = 0;

        AppendRegion(context, context.Frame.Header, builder, ref lines);
        AppendRegion(context, context.Frame.Body, builder, ref lines);
        AppendRegion(context, context.Frame.Footer, builder, ref lines);

        var markup = builder.ToString();

        if (builder.Capacity > MaxRetainedCapacity)
        {
            scratch = null;
        }

        return markup;
    }

    private void AppendRegion( IMenuRenderContext context, IReadOnlyList<MenuNode> nodes, StringBuilder builder, ref int lines )
    {
        foreach (var node in nodes)
        {
            AppendNode(context, node, builder, ref lines);
        }
    }

    private void AppendNode( IMenuRenderContext context, MenuNode node, StringBuilder builder, ref int lines )
    {
        switch (node)
        {
            case MenuBlankNode blank:
                for (var index = 0; index < blank.Lines; index++)
                {
                    StartLine(builder, ref lines);
                }

                break;

            case MenuStackNode stack:
                foreach (var child in stack.Children)
                {
                    AppendNode(context, child, builder, ref lines);
                }

                break;

            default:
                var mark = builder.Length;

                if (lines > 0)
                {
                    _ = builder.Append("<br>");
                }

                if (AppendInline(context, node, builder))
                {
                    lines++;
                }
                else
                {
                    builder.Length = mark;
                }

                break;
        }
    }

    private static void StartLine( StringBuilder builder, ref int lines )
    {
        if (lines > 0)
        {
            _ = builder.Append("<br>");
        }

        lines++;
    }

    private bool AppendInline( IMenuRenderContext context, MenuNode node, StringBuilder builder )
    {
        switch (node)
        {
            case MenuTextNode text:
                Wrap(builder, text.Text, text.Style);
                return true;

            case MenuSelectionNode selection:
                if (selection.Number > 0)
                {
                    _ = builder.Append(selection.Number).Append(". ");
                    return true;
                }

                _ = builder.Append(selection.Focused ? SelectionMarker : SelectionPadding);
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

    private static void Wrap( StringBuilder builder, string text, MenuTextStyle style )
    {
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
