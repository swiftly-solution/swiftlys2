using SwiftlyS2.Core.Menu.Config;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuFrameComposer( MenuRendererRegistry renderers, MenuLayoutOptions layout )
{
    public MenuFrame Compose( MenuSession session )
    {
        var menu = session.Instance;
        var rendererId = menu.Renderer.Id;

        var header = RenderRegion(menu, session, MenuRegion.Header, rendererId);
        var footer = RenderRegion(menu, session, MenuRegion.Footer, rendererId);

        var body = menu.GetVisible(MenuRegion.Body, session);
        var focusables = menu.GetFocusables(session);
        var focused = focusables.Count == 0 ? null : focusables[Math.Clamp(session.FocusedIndex, 0, focusables.Count - 1)];

        var page = ResolvePage(body, focused, menu.ItemsPerPage, layout.MaxVisibleLines, session.PageOffset);
        session.PageOffset = page.Offset;

        var bodyNodes = new List<MenuNode>(page.Count);

        for (var index = page.Offset; index < page.Offset + page.Count; index++)
        {
            var component = body[index];
            var isFocused = ReferenceEquals(component, focused);
            var node = RenderComponent(menu, session, component, isFocused, rendererId);

            bodyNodes.Add(component.IsFocusable
                ? MenuLineNode.Of(new MenuSelectionNode(isFocused), node)
                : node);
        }

        if (focused?.GetHint(session) is { Length: > 0 } hint)
        {
            footer.Add(new MenuTextNode(hint, MenuTextStyle.Default.WithSize(MenuTextSize.Small)));
        }

        var hints = BuildKeybindHints(menu);

        if (hints is not null)
        {
            footer.Add(hints);
        }

        return new MenuFrame {
            Header = header,
            Body = bodyNodes,
            Footer = footer,
            PageOffset = page.Offset,
            TotalItems = focusables.Count
        };
    }

    private List<MenuNode> RenderRegion( MenuInstance menu, MenuSession session, MenuRegion region, string rendererId )
    {
        var components = menu.GetVisible(region, session);
        var nodes = new List<MenuNode>(components.Count);

        foreach (var component in components)
        {
            nodes.Add(RenderComponent(menu, session, component, false, rendererId));
        }

        return nodes;
    }

    private MenuNode RenderComponent(
        MenuInstance menu,
        MenuSession session,
        IMenuComponent component,
        bool isFocused,
        string rendererId )
    {
        var context = new ComponentRenderContext(
            menu,
            session,
            component,
            isFocused,
            component.IsEnabled(session),
            rendererId);

        var specialised = renderers.ResolveComponentRenderer(rendererId, component.GetType());

        if (specialised is not null && specialised.TryRender(component, context, out var node) && node is not null)
        {
            return node;
        }

        return component.Render(context);
    }

    private static (int Offset, int Count) ResolvePage(
        List<IMenuComponent> body,
        IMenuComponent? focused,
        int itemsPerPage,
        int maxVisibleLines,
        int currentOffset )
    {
        if (body.Count == 0)
        {
            return (0, 0);
        }

        var offset = Math.Clamp(currentOffset, 0, body.Count - 1);
        var position = focused is null ? -1 : body.IndexOf(focused);

        if (position >= 0 && position < offset)
        {
            offset = position;
        }

        var count = Measure(body, offset, itemsPerPage, maxVisibleLines);

        while (position >= offset + count)
        {
            offset++;
            count = Measure(body, offset, itemsPerPage, maxVisibleLines);
        }

        while (offset > 0 && offset + count >= body.Count)
        {
            var earlier = Measure(body, offset - 1, itemsPerPage, maxVisibleLines);

            if (earlier <= count)
            {
                break;
            }

            offset--;
            count = earlier;
        }

        return (offset, count);
    }

    private static int Measure( List<IMenuComponent> body, int offset, int itemsPerPage, int maxVisibleLines )
    {
        var count = 0;
        var lines = 0;

        for (var index = offset; index < body.Count && count < itemsPerPage; index++)
        {
            var cost = Math.Max(1, body[index].LineCount);

            if (count > 0 && lines + cost > maxVisibleLines)
            {
                break;
            }

            lines += cost;
            count++;
        }

        return Math.Max(1, count);
    }

    private static readonly MenuTextStyle FooterLabelStyle = MenuTextStyle.Default.WithSize(MenuTextSize.Small).WithColor("#FF0000");
    private static readonly MenuTextStyle FooterValueStyle = MenuTextStyle.Default.WithSize(MenuTextSize.Small).WithColor("#FFFFFF");

    private static MenuNode? BuildKeybindHints( MenuInstance menu )
    {
        var parts = new List<MenuNode>();

        foreach (var descriptor in menu.Keymap.Actions)
        {
            if (!descriptor.ShowInFooter)
            {
                continue;
            }

            var key = menu.Keymap.GetKey(descriptor.Id);

            if (key == MenuKey.None)
            {
                continue;
            }

            if (parts.Count > 0)
            {
                parts.Add(new MenuTextNode(" | ", FooterValueStyle));
            }

            parts.Add(new MenuTextNode($"{descriptor.Label ?? descriptor.Id.Name}: ", FooterLabelStyle));
            parts.Add(new MenuTextNode(MenuKeys.Describe(key), FooterValueStyle));
        }

        return parts.Count == 0 ? null : new MenuLineNode(parts);
    }
}
