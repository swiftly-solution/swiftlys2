using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line that cycles through a list one item at a time.
/// </summary>
/// <typeparam name="TItem">The item type.</typeparam>
/// <remarks>
/// Activating moves forward by one and wraps. For a component that steps both ways with its own
/// keys, use <see cref="SelectorComponent{TItem}"/>.
/// </remarks>
public class ChoiceComponent<TItem> : MenuChoiceComponentBase<TItem>
{
    /// <summary>
    /// Creates a choice.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="choices">The items to cycle through.</param>
    /// <param name="defaultIndex">The index a player starts on.</param>
    /// <param name="onItemChanged">Runs after the selection moves.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ChoiceComponent(
        string text = "",
        IEnumerable<TItem>? choices = null,
        int defaultIndex = 0,
        Action<MenuValueChangedContext<TItem>>? onItemChanged = null,
        string? id = null ) : base(text, choices, defaultIndex, id)
    {
        OnItemChanged = onItemChanged;
    }

    /// <summary>
    /// The bracket drawn before the value.
    /// </summary>
    public string OpeningBracket { get; set; } = "[";

    /// <summary>
    /// The bracket drawn after the value.
    /// </summary>
    public string ClosingBracket { get; set; } = "]";

    /// <summary>
    /// The colour of the opening bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? OpeningBracketColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// The colour of the closing bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? ClosingBracketColor { get; set; } = "#FF3333";

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        if (RenderWaiting(context) is { } waiting)
        {
            return waiting;
        }

        var parts = new List<MenuNode>(4);

        if (RenderLabel(context) is { } label)
        {
            parts.Add(label);
        }

        parts.Add(RenderValue(context, OpeningBracket, OpeningBracketColor));
        parts.Add(RenderValue(context, Format(GetValue(context.Session))));
        parts.Add(RenderValue(context, ClosingBracket, ClosingBracketColor));

        return new MenuLineNode(parts);
    }

    /// <inheritdoc/>
    public override async ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        if (!Matches(context, MenuActions.Select))
        {
            return false;
        }

        return await ActivateAsync(context, ctx => {
            _ = Step(ctx.Session, 1);
            return ValueTask.CompletedTask;
        });
    }
}

/// <summary>
/// A selectable line that cycles through a list of strings.
/// </summary>
public class ChoiceComponent : ChoiceComponent<string>
{
    /// <summary>
    /// Creates a choice.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="choices">The strings to cycle through.</param>
    /// <param name="defaultIndex">The index a player starts on.</param>
    /// <param name="onItemChanged">Runs after the selection moves.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ChoiceComponent(
        string text = "",
        IEnumerable<string>? choices = null,
        int defaultIndex = 0,
        Action<MenuValueChangedContext<string>>? onItemChanged = null,
        string? id = null ) : base(text, choices, defaultIndex, onItemChanged, id) { }
}
