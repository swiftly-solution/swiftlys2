using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line carrying an on/off value.
/// </summary>
public class ToggleComponent : MenuValueComponent<bool>
{
    /// <summary>
    /// Creates a toggle.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="defaultValue">The value a player starts with.</param>
    /// <param name="onChanged">Runs after the value flips.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ToggleComponent(
        string text = "",
        bool defaultValue = false,
        Action<MenuValueChangedContext<bool>>? onChanged = null,
        string? id = null ) : base(text, id)
    {
        DefaultValue = defaultValue;
        OnChanged = onChanged;
        Separator = " ";
    }

    /// <summary>
    /// The symbol shown while the value is on.
    /// </summary>
    public string OnSymbol { get; set; } = "✔";

    /// <summary>
    /// The symbol shown while the value is off.
    /// </summary>
    public string OffSymbol { get; set; } = "✘";

    /// <summary>
    /// The colour of <see cref="OnSymbol"/>.
    /// </summary>
    public string? OnColor { get; set; } = "#008000";

    /// <summary>
    /// The colour of <see cref="OffSymbol"/>.
    /// </summary>
    public string? OffColor { get; set; } = "#FF0000";

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        if (RenderWaiting(context) is { } waiting)
        {
            return waiting;
        }

        var value = GetValue(context.Session);
        var symbol = RenderValue(context, value ? OnSymbol : OffSymbol, value ? OnColor : OffColor);
        var label = RenderLabel(context);

        return label is null ? symbol : MenuLineNode.Of(label, symbol);
    }

    /// <inheritdoc/>
    public override async ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        if (!Matches(context, MenuActions.Select))
        {
            return false;
        }

        return await ActivateAsync(context, ctx => {
            _ = SetValue(ctx.Session, !GetValue(ctx.Session));
            return ValueTask.CompletedTask;
        });
    }
}
