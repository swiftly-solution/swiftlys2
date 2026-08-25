using SwiftlyS2.Shared.Menu;

using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// Describes a value one player changed on a component.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public readonly record struct MenuValueChangedContext<TValue>
{
    /// <summary>
    /// The session the value belongs to.
    /// </summary>
    public required IMenuSession Session { get; init; }

    /// <summary>
    /// The component that changed.
    /// </summary>
    public required IMenuComponent Component { get; init; }

    /// <summary>
    /// The value before the change.
    /// </summary>
    public required TValue OldValue { get; init; }

    /// <summary>
    /// The value after the change.
    /// </summary>
    public required TValue NewValue { get; init; }

    /// <summary>
    /// The player who made the change.
    /// </summary>
    public IPlayer Player => Session.Player;
}

/// <summary>
/// One player's value for one component.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public sealed class MenuValueState<TValue>
{
    /// <summary>
    /// Whether the value has been seeded from the component's default.
    /// </summary>
    public bool Initialised { get; set; }

    /// <summary>
    /// The stored value.
    /// </summary>
    public TValue? Value { get; set; }
}

/// <summary>
/// The shared behaviour of a component that holds a value.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
/// <remarks>
/// The value lives in <see cref="IMenuSession"/> state, so one instance of the component serves
/// every player without their values touching, and the value is gone once the menu closes. Use
/// <see cref="OnChanged"/> to persist it and <see cref="DefaultValueProvider"/> to seed it back.
/// </remarks>
public abstract class MenuValueComponent<TValue> : TextComponent
{
    /// <summary>
    /// Creates a value component.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    protected MenuValueComponent( string text = "", string? id = null ) : base(text, id) { }

    /// <inheritdoc/>
    public override bool IsFocusable => true;

    /// <summary>
    /// The value a player starts with when <see cref="DefaultValueProvider"/> is null.
    /// </summary>
    public TValue DefaultValue { get; set; } = default!;

    /// <summary>
    /// Produces the value a player starts with, or null to use <see cref="DefaultValue"/>.
    /// </summary>
    /// <remarks>
    /// Called once per session, on first access. This is where a stored value is read back.
    /// </remarks>
    public Func<IMenuSession, TValue>? DefaultValueProvider { get; set; }

    /// <summary>
    /// Runs after a player changes the value.
    /// </summary>
    public Action<MenuValueChangedContext<TValue>>? OnChanged { get; set; }

    /// <summary>
    /// The separator drawn between the label and the value.
    /// </summary>
    public string Separator { get; set; } = ": ";

    /// <summary>
    /// The colour of the value, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? ValueColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// How values are compared to decide whether a change happened.
    /// </summary>
    protected virtual IEqualityComparer<TValue> Comparer => EqualityComparer<TValue>.Default;

    /// <summary>
    /// Reads this player's value, seeding it from the default on first use.
    /// </summary>
    /// <param name="session">The session to read for.</param>
    /// <returns>The current value.</returns>
    public TValue GetValue( IMenuSession session )
    {
        var state = session.GetState<MenuValueState<TValue>>(this);

        if (!state.Initialised)
        {
            state.Value = Coerce(session, DefaultValueProvider is not null ? DefaultValueProvider(session) : DefaultValue);
            state.Initialised = true;
        }

        return state.Value!;
    }

    /// <summary>
    /// Writes this player's value.
    /// </summary>
    /// <param name="session">The session to write for.</param>
    /// <param name="value">The value to store. Components may adjust it before it is stored.</param>
    /// <returns><see langword="true"/> when the stored value actually changed.</returns>
    public bool SetValue( IMenuSession session, TValue value )
    {
        var current = GetValue(session);
        var coerced = Coerce(session, value);

        if (Comparer.Equals(current, coerced))
        {
            return false;
        }

        session.GetState<MenuValueState<TValue>>(this).Value = coerced;

        OnChanged?.Invoke(new MenuValueChangedContext<TValue> {
            Session = session,
            Component = this,
            OldValue = current,
            NewValue = coerced
        });

        session.Invalidate();
        return true;
    }

    /// <summary>
    /// Adjusts a value before it is stored.
    /// </summary>
    /// <param name="session">The session the value belongs to.</param>
    /// <param name="value">The value being stored.</param>
    /// <returns>The value to store.</returns>
    /// <remarks>
    /// Used for clamping into a range or into the bounds of a list.
    /// </remarks>
    protected virtual TValue Coerce( IMenuSession session, TValue value ) => value;

    /// <summary>
    /// Builds the label part of this component's line.
    /// </summary>
    /// <param name="context">The context being drawn.</param>
    /// <returns>The label node, or null when there is no label.</returns>
    protected MenuNode? RenderLabel( IMenuComponentRenderContext context )
    {
        var label = ResolveText(context.Session);

        return string.IsNullOrEmpty(label)
            ? null
            : new MenuTextNode($"{label}{Separator}", ResolveStyle(context));
    }

    /// <summary>
    /// Builds a node for a value, greyed out when the component is disabled.
    /// </summary>
    /// <param name="context">The context being drawn.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="color">The colour to use while enabled, or null for <see cref="ValueColor"/>.</param>
    /// <returns>The value node.</returns>
    protected MenuNode RenderValue( IMenuComponentRenderContext context, string text, string? color = null )
    {
        var resolved = context.IsEnabled ? color ?? ValueColor : DisabledColor;

        return new MenuTextNode(text, resolved is null ? Style : Style.WithColor(resolved));
    }
}
