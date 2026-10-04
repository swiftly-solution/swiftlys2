using SwiftlyS2.Shared.Menu;

namespace TestPlugin;

public sealed class ClickCounterComponent( string text, int maxClicks = 5 ) : MenuComponentBase
{
    public override bool IsFocusable => true;

    public string Text { get; set; } = text;

    public int MaxClicks { get; set; } = maxClicks;

    public override bool IsEnabled( IMenuSession session )
        => base.IsEnabled(session) && session.GetState<ClickState>(this).Clicks < MaxClicks;

    public override string? GetHint( IMenuSession session )
    {
        var remaining = MaxClicks - session.GetState<ClickState>(this).Clicks;
        return remaining > 0 ? $"{remaining} click(s) left" : "No clicks left";
    }

    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        if (IsBusy(context.Session))
        {
            return new MenuTextNode(WaitingText, MenuTextStyle.Default.WithColor(WaitingColor));
        }

        var clicks = context.Session.GetState<ClickState>(this).Clicks;
        var labelStyle = MenuTextStyle.Default.WithColor(context.IsEnabled ? "#FFFFFF" : "#666666");
        var countStyle = labelStyle.WithColor(clicks == 0 ? "#999999" : "#C0FF3E");

        return MenuLineNode.Of(
            new MenuTextNode($"{Text}: ", labelStyle),
            new MenuTextNode($"{clicks} click(s)", countStyle));
    }

    public override ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        if (!Matches(context, MenuActions.Select))
        {
            return ValueTask.FromResult(false);
        }

        return ActivateAsync(context, ctx => {
            var state = ctx.Session.GetState<ClickState>(this);
            state.Clicks++;

            ctx.Player.SendChat($"{Text} clicked {state.Clicks} time(s).");
            ctx.Session.Invalidate();
            return ValueTask.CompletedTask;
        });
    }

    private sealed class ClickState
    {
        public int Clicks { get; set; }
    }
}
