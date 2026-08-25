namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// The recommended starting point for a component.
/// </summary>
/// <remarks>
/// Supplies identity, visibility and enabled rules, the activation pipeline shared by every
/// interactive component, and a no-op action handler, leaving <see cref="Render"/> as the only
/// required member.
/// </remarks>
public abstract class MenuComponentBase : IMenuComponent
{
    /// <summary>
    /// Creates a component.
    /// </summary>
    /// <param name="id">A stable id, or null to generate one.</param>
    protected MenuComponentBase( string? id = null )
    {
        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString("N") : id;
    }

    /// <inheritdoc/>
    public string Id { get; }

    /// <inheritdoc/>
    public virtual bool IsFocusable => false;

    /// <summary>
    /// Whether this component is shown, for every player.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Whether this component accepts actions, for every player.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Decides visibility per player, or null to use <see cref="Visible"/>.
    /// </summary>
    /// <remarks>
    /// Evaluated on every frame. This is how one component instance is hidden from some players and
    /// not others without holding any per-player state itself.
    /// </remarks>
    public Func<IMenuSession, bool>? VisibleWhen { get; set; }

    /// <summary>
    /// Decides interactivity per player, or null to use <see cref="Enabled"/>.
    /// </summary>
    public Func<IMenuSession, bool>? EnabledWhen { get; set; }

    /// <summary>
    /// The hint shown while this component holds the selection, or null for none.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Whether activating this component plays the menu's selection sound.
    /// </summary>
    public bool PlaySound { get; set; } = true;

    /// <inheritdoc/>
    public virtual int LineCount => 1;

    /// <summary>
    /// Runs before an activation and can cancel it by returning <see langword="false"/>.
    /// </summary>
    public Func<MenuActionContext, ValueTask<bool>>? Validating { get; set; }

    /// <summary>
    /// The text shown in place of this component's own while an activation is still running.
    /// </summary>
    public string WaitingText { get; set; } = "Waiting...";

    /// <summary>
    /// The colour applied to <see cref="WaitingText"/>, or null to keep the normal style.
    /// </summary>
    public string? WaitingColor { get; set; } = "#C0FF3E";

    /// <summary>
    /// An arbitrary value carried alongside this component.
    /// </summary>
    public object? Tag { get; set; }

    /// <inheritdoc/>
    public virtual bool IsVisible( IMenuSession session ) => Visible && (VisibleWhen?.Invoke(session) ?? true);

    /// <inheritdoc/>
    public virtual bool IsEnabled( IMenuSession session ) => Enabled && (EnabledWhen?.Invoke(session) ?? true);

    /// <inheritdoc/>
    public virtual string? GetHint( IMenuSession session ) => Comment;

    /// <inheritdoc/>
    public virtual bool NeedsRedraw( IMenuSession session, DateTime now ) => false;

    /// <inheritdoc/>
    public abstract MenuNode Render( IMenuComponentRenderContext context );

    /// <inheritdoc/>
    public virtual ValueTask<bool> HandleActionAsync( MenuActionContext context ) => ValueTask.FromResult(false);

    /// <summary>
    /// Whether an activation started by this player is still running.
    /// </summary>
    /// <param name="session">The session to check.</param>
    /// <returns><see langword="true"/> while the previous activation has not finished.</returns>
    protected bool IsBusy( IMenuSession session ) => session.GetState<MenuActivationState>(this).IsBusy;

    /// <summary>
    /// Runs an activation through the shared pipeline.
    /// </summary>
    /// <param name="context">The action that triggered the activation.</param>
    /// <param name="body">The work to run once the activation is allowed.</param>
    /// <returns><see langword="true"/> once the action has been consumed.</returns>
    /// <remarks>
    /// Refuses when the component is disabled or a previous activation for the same player is still
    /// running, then consults <see cref="Validating"/>. While <paramref name="body"/> runs the
    /// component draws <see cref="WaitingText"/>, so a slow callback cannot be triggered twice.
    /// </remarks>
    protected async ValueTask<bool> ActivateAsync( MenuActionContext context, Func<MenuActionContext, ValueTask> body )
    {
        var session = context.Session;
        var state = session.GetState<MenuActivationState>(this);

        if (!IsEnabled(session) || state.IsBusy)
        {
            return true;
        }

        if (Validating is not null && !await Validating(context))
        {
            return true;
        }

        state.IsBusy = true;
        session.Invalidate();

        try
        {
            await body(context);
        }
        finally
        {
            state.IsBusy = false;
            session.Invalidate();
        }

        return true;
    }

    /// <summary>
    /// Whether an action matches a built-in or declared action by name.
    /// </summary>
    /// <param name="context">The dispatched action.</param>
    /// <param name="action">The action to compare against.</param>
    /// <returns><see langword="true"/> when the names match.</returns>
    protected static bool Matches( MenuActionContext context, MenuActionId action )
        => string.Equals(context.Action.Name, action.Name, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Per-player activation bookkeeping.
    /// </summary>
    protected sealed class MenuActivationState
    {
        /// <summary>
        /// Whether an activation is running right now.
        /// </summary>
        public bool IsBusy { get; set; }
    }
}
