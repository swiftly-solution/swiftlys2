using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// The shared behaviour of a component that picks one item out of a list.
/// </summary>
/// <typeparam name="TItem">The item type.</typeparam>
/// <remarks>
/// The stored value is the index, which keeps duplicate items in the list unambiguous. Use
/// <see cref="GetItem"/> and <see cref="OnItemChanged"/> to work in terms of items instead.
/// </remarks>
public abstract class MenuChoiceComponentBase<TItem> : MenuValueComponent<int>
{
    /// <summary>
    /// Creates a choice component.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="choices">The items to pick from.</param>
    /// <param name="defaultIndex">The index a player starts on.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    protected MenuChoiceComponentBase(
        string text = "",
        IEnumerable<TItem>? choices = null,
        int defaultIndex = 0,
        string? id = null ) : base(text, id)
    {
        Choices = choices?.ToList() ?? [];
        DefaultValue = defaultIndex;
    }

    /// <summary>
    /// The items to pick from.
    /// </summary>
    public IReadOnlyList<TItem> Choices { get; set; }

    /// <summary>
    /// Turns an item into the text shown for it.
    /// </summary>
    public Func<TItem, string> Formatter { get; set; } = item => item?.ToString() ?? string.Empty;

    /// <summary>
    /// Whether stepping past either end continues from the other.
    /// </summary>
    public bool WrapAround { get; set; } = true;

    /// <summary>
    /// The text shown when <see cref="Choices"/> is empty.
    /// </summary>
    public string EmptyText { get; set; } = "N/A";

    /// <summary>
    /// Runs after a player moves to a different item.
    /// </summary>
    public Action<MenuValueChangedContext<TItem>>? OnItemChanged { get; set; }

    /// <summary>
    /// Reads the item this player is on.
    /// </summary>
    /// <param name="session">The session to read for.</param>
    /// <returns>The selected item, or the type default when the list is empty.</returns>
    public TItem? GetItem( IMenuSession session )
    {
        var index = GetValue(session);
        return Choices.Count == 0 ? default : Choices[index];
    }

    /// <summary>
    /// Moves this player to a different item.
    /// </summary>
    /// <param name="session">The session to move.</param>
    /// <param name="delta">How far to move. Negative moves towards the start.</param>
    /// <returns><see langword="true"/> when the selection changed.</returns>
    public bool Step( IMenuSession session, int delta )
    {
        if (Choices.Count == 0 || delta == 0)
        {
            return false;
        }

        var current = GetValue(session);
        var target = WrapAround
            ? ((current + delta) % Choices.Count + Choices.Count) % Choices.Count
            : Math.Clamp(current + delta, 0, Choices.Count - 1);

        if (target == current)
        {
            return false;
        }

        var previous = Choices[current];

        if (!SetValue(session, target))
        {
            return false;
        }

        OnItemChanged?.Invoke(new MenuValueChangedContext<TItem> {
            Session = session,
            Component = this,
            OldValue = previous,
            NewValue = Choices[target]
        });

        return true;
    }

    /// <summary>
    /// Formats the item at an index, tolerating an empty list.
    /// </summary>
    /// <param name="index">The index to format.</param>
    /// <returns>The formatted item, or <see cref="EmptyText"/>.</returns>
    protected string Format( int index )
    {
        return Choices.Count == 0 || index < 0 || index >= Choices.Count
            ? EmptyText
            : Formatter(Choices[index]);
    }

    /// <inheritdoc/>
    protected override int Coerce( IMenuSession session, int value )
        => Choices.Count == 0 ? 0 : Math.Clamp(value, 0, Choices.Count - 1);
}
