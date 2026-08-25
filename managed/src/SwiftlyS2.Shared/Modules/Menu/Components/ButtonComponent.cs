using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line that runs a callback when activated.
/// </summary>
/// <remarks>
/// Activation goes through the shared pipeline, so <see cref="MenuComponentBase.Validating"/> can
/// veto it and a slow <see cref="OnSelect"/> cannot be started twice by the same player.
/// </remarks>
public class ButtonComponent : TextComponent
{
    /// <summary>
    /// Creates a button.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="onSelect">The callback to run when the button is activated.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ButtonComponent( string text = "", Func<MenuActionContext, ValueTask>? onSelect = null, string? id = null )
        : base(text, id)
    {
        OnSelect = onSelect;
    }

    /// <inheritdoc/>
    public override bool IsFocusable => true;

    /// <summary>
    /// Runs when the button is activated.
    /// </summary>
    public Func<MenuActionContext, ValueTask>? OnSelect { get; set; }

    /// <summary>
    /// Whether activating the button also closes the menu.
    /// </summary>
    public bool CloseOnSelect { get; set; }

    /// <summary>
    /// The colour applied when the button is focused, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? FocusedColor { get; set; }

    /// <inheritdoc/>
    public override async ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        if (!Matches(context, MenuActions.Select))
        {
            return false;
        }

        return await ActivateAsync(context, async ctx => {
            if (OnSelect is not null)
            {
                await OnSelect(ctx);
            }

            if (CloseOnSelect)
            {
                ctx.Session.Close();
            }
        });
    }

    /// <inheritdoc/>
    protected override MenuTextStyle ResolveStyle( IMenuComponentRenderContext context )
    {
        var style = base.ResolveStyle(context);

        return context.IsEnabled && context.IsFocused && FocusedColor is not null
            ? style.WithColor(FocusedColor)
            : style;
    }
}
