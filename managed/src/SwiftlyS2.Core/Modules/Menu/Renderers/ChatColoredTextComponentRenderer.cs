using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Menu.Components;

namespace SwiftlyS2.Core.Menu.Renderers;

internal sealed class ChatColoredTextComponentRenderer : IComponentRenderer
{
    public string RendererId => MenuRendererIds.Chat;

    public Type ComponentType => typeof(ChatColoredTextComponent);

    public bool TryRender( IMenuComponent component, IMenuComponentRenderContext context, out MenuNode? node )
    {
        if (component is not ChatColoredTextComponent chatText)
        {
            node = null;
            return false;
        }

        node = new MenuRawNode(MenuRendererIds.Chat, $"{chatText.ChatColor}{chatText.Text}".Colored());
        return true;
    }
}
