using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line that steps through a list with its own left and right keys.
/// </summary>
/// <typeparam name="TItem">The item type.</typeparam>
/// <remarks>
/// Takes over <see cref="MenuActions.NavigateLeft"/> and <see cref="MenuActions.NavigateRight"/>
/// while it holds the selection, and shows the neighbouring items either side of the current one.
/// </remarks>
public class SelectorComponent<TItem> : MenuChoiceComponentBase<TItem>
{
    /// <summary>
    /// Creates a selector.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="choices">The items to step through.</param>
    /// <param name="defaultIndex">The index a player starts on.</param>
    /// <param name="onItemChanged">Runs after the selection moves.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SelectorComponent(
        string text = "",
        IEnumerable<TItem>? choices = null,
        int defaultIndex = 0,
        Action<MenuValueChangedContext<TItem>>? onItemChanged = null,
        string? id = null ) : base(text, choices, defaultIndex, id)
    {
        OnItemChanged = onItemChanged;
        Comment = "Left/Right to change";
    }

    /// <summary>
    /// Whether the items either side of the current one are shown.
    /// </summary>
    public bool ShowNeighbours { get; set; } = true;

    /// <summary>
    /// The colour of the neighbouring items.
    /// </summary>
    public string? NeighbourColor { get; set; } = "#666666";

    /// <summary>
    /// The bracket drawn before the current item.
    /// </summary>
    public string OpeningBracket { get; set; } = "[";

    /// <summary>
    /// The bracket drawn after the current item.
    /// </summary>
    public string ClosingBracket { get; set; } = "]";

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        if (RenderWaiting(context) is { } waiting)
        {
            return waiting;
        }

        var index = GetValue(context.Session);
        var parts = new List<MenuNode>(6);

        if (RenderLabel(context) is { } label)
        {
            parts.Add(label);
        }

        if (ShowNeighbours && Choices.Count > 1)
        {
            parts.Add(RenderValue(context, $"{Format(Neighbour(index, -1))} ", NeighbourColor));
        }

        parts.Add(RenderValue(context, OpeningBracket));
        parts.Add(RenderValue(context, Format(index)));
        parts.Add(RenderValue(context, ClosingBracket));

        if (ShowNeighbours && Choices.Count > 1)
        {
            parts.Add(RenderValue(context, $" {Format(Neighbour(index, 1))}", NeighbourColor));
        }

        return new MenuLineNode(parts);
    }

    /// <inheritdoc/>
    public override async ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        var delta = Matches(context, MenuActions.NavigateLeft) ? -1
            : Matches(context, MenuActions.NavigateRight) ? 1
            : 0;

        if (delta == 0)
        {
            return false;
        }

        return await ActivateAsync(context, ctx => {
            _ = Step(ctx.Session, delta);
            return ValueTask.CompletedTask;
        });
    }

    private int Neighbour( int index, int delta )
        => Choices.Count == 0 ? 0 : ((index + delta) % Choices.Count + Choices.Count) % Choices.Count;
}
