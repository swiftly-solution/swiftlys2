using SwiftlyS2.Shared.Menu;

using System.Globalization;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line carrying a number, drawn as a bar.
/// </summary>
/// <remarks>
/// Takes over <see cref="MenuActions.NavigateLeft"/> and <see cref="MenuActions.NavigateRight"/>
/// while it holds the selection; activating steps up by one <see cref="Step"/>.
/// </remarks>
public class SliderComponent : MenuValueComponent<float>
{
    private float step = 5f;
    private int totalBars = 10;

    /// <summary>
    /// Creates a slider.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="min">The lowest value.</param>
    /// <param name="max">The highest value.</param>
    /// <param name="defaultValue">The value a player starts with, or null for <paramref name="min"/>.</param>
    /// <param name="step">How far one press moves the value.</param>
    /// <param name="onChanged">Runs after the value changes.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SliderComponent(
        string text = "",
        float min = 0f,
        float max = 100f,
        float? defaultValue = null,
        float step = 5f,
        Action<MenuValueChangedContext<float>>? onChanged = null,
        string? id = null ) : base(text, id)
    {
        Min = Math.Min(min, max);
        Max = Math.Max(min, max);
        Step = step;
        DefaultValue = defaultValue ?? Min;
        OnChanged = onChanged;
        Comment = "Left/Right to change";
    }

    /// <summary>
    /// The lowest value.
    /// </summary>
    public float Min { get; set; }

    /// <summary>
    /// The highest value.
    /// </summary>
    public float Max { get; set; } = 100f;

    /// <summary>
    /// How far one press moves the value. Never zero or negative.
    /// </summary>
    public float Step {
        get => step;
        set => step = value <= 0f ? 1f : value;
    }

    /// <summary>
    /// How many segments the bar is drawn with. Never below one.
    /// </summary>
    public int TotalBars {
        get => totalBars;
        set => totalBars = Math.Max(1, value);
    }

    /// <summary>
    /// Whether stepping past either end continues from the other.
    /// </summary>
    public bool WrapAround { get; set; } = true;

    /// <summary>
    /// The character drawn for a filled segment.
    /// </summary>
    public string FilledChar { get; set; } = "■";

    /// <summary>
    /// The character drawn for an empty segment.
    /// </summary>
    public string EmptyChar { get; set; } = "□";

    /// <summary>
    /// The colour of the empty segments.
    /// </summary>
    public string? EmptyColor { get; set; } = "#666666";

    /// <summary>
    /// The bracket drawn before the bar.
    /// </summary>
    public string OpeningBracket { get; set; } = "(";

    /// <summary>
    /// The bracket drawn after the bar.
    /// </summary>
    public string ClosingBracket { get; set; } = ")";

    /// <summary>
    /// The colour of the opening bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? OpeningBracketColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// The colour of the closing bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? ClosingBracketColor { get; set; } = "#FF3333";

    /// <summary>
    /// Whether the number itself is shown next to the bar.
    /// </summary>
    public bool ShowValue { get; set; } = true;

    /// <summary>
    /// How the number is formatted.
    /// </summary>
    public string ValueFormat { get; set; } = "F1";

    /// <summary>
    /// Moves this player's value by one <see cref="Step"/>.
    /// </summary>
    /// <param name="session">The session to move.</param>
    /// <param name="direction">Positive to step up, negative to step down.</param>
    /// <returns><see langword="true"/> when the value changed.</returns>
    public bool StepValue( IMenuSession session, int direction )
    {
        if (direction == 0)
        {
            return false;
        }

        var current = GetValue(session);
        var target = current + Step * direction;

        if (WrapAround)
        {
            if (target > Max)
            {
                target = Min;
            }
            else if (target < Min)
            {
                target = Max;
            }
        }

        return SetValue(session, target);
    }

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        if (RenderWaiting(context) is { } waiting)
        {
            return waiting;
        }

        var value = GetValue(context.Session);
        var span = Max - Min;
        var progress = span <= 0f ? 0f : Math.Clamp((value - Min) / span, 0f, 1f);
        var filled = (int)(progress * TotalBars);

        var parts = new List<MenuNode>(6);

        if (RenderLabel(context) is { } label)
        {
            parts.Add(label);
        }

        parts.Add(RenderValue(context, OpeningBracket, OpeningBracketColor));

        if (filled > 0)
        {
            parts.Add(RenderValue(context, string.Concat(Enumerable.Repeat(FilledChar, filled))));
        }

        if (filled < TotalBars)
        {
            parts.Add(RenderValue(context, string.Concat(Enumerable.Repeat(EmptyChar, TotalBars - filled)), EmptyColor));
        }

        parts.Add(RenderValue(context, ClosingBracket, ClosingBracketColor));

        if (ShowValue)
        {
            parts.Add(RenderValue(context, $" {value.ToString(ValueFormat, CultureInfo.InvariantCulture)}"));
        }

        return new MenuLineNode(parts);
    }

    /// <inheritdoc/>
    public override async ValueTask<bool> HandleActionAsync( MenuActionContext context )
    {
        var direction = Matches(context, MenuActions.NavigateLeft) ? -1
            : Matches(context, MenuActions.NavigateRight) || Matches(context, MenuActions.Select) ? 1
            : 0;

        if (direction == 0)
        {
            return false;
        }

        return await ActivateAsync(context, ctx => {
            _ = StepValue(ctx.Session, direction);
            return ValueTask.CompletedTask;
        });
    }

    /// <inheritdoc/>
    protected override float Coerce( IMenuSession session, float value )
        => Math.Clamp(value, Math.Min(Min, Max), Math.Max(Min, Max));
}
