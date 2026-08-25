using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line that opens another menu.
/// </summary>
/// <remarks>
/// The submenu is produced by a factory rather than held directly, so it can be built lazily from
/// the state of the player opening it. Closing the submenu returns the player here: a menu that has
/// no parent of its own is adopted by this one when it opens.
/// </remarks>
public class SubmenuComponent : TextComponent
{
    private readonly Func<IMenuSession, ValueTask<IMenu?>> factory;

    /// <summary>
    /// Creates a submenu entry from an already built menu.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="submenu">The menu to open.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SubmenuComponent( string text, IMenu submenu, string? id = null )
        : this(text, _ => ValueTask.FromResult<IMenu?>(submenu), id) { }

    /// <summary>
    /// Creates a submenu entry from a factory.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="factory">Produces the menu to open. Returning null cancels the navigation.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SubmenuComponent( string text, Func<IMenuSession, IMenu?> factory, string? id = null )
        : this(text, session => ValueTask.FromResult(factory(session)), id) { }

    /// <summary>
    /// Creates a submenu entry from an asynchronous factory.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="factory">Produces the menu to open. Returning null cancels the navigation.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SubmenuComponent( string text, Func<IMenuSession, ValueTask<IMenu?>> factory, string? id = null )
        : base(text, id)
    {
        this.factory = factory;
    }

    /// <inheritdoc/>
    public override bool IsFocusable => true;

    /// <summary>
    /// The colour applied when the entry is focused, or null to keep <see cref="TextComponent.Style"/>.
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
            var submenu = await factory(ctx.Session);

            if (submenu is null || !ctx.Session.IsOpen)
            {
                return;
            }

            if (submenu.Parent is null && !ReferenceEquals(submenu, ctx.Menu))
            {
                submenu.Parent = ctx.Menu;
            }

            _ = submenu.Open(ctx.Player);
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
