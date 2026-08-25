using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A selectable line whose value the player types into chat.
/// </summary>
/// <remarks>
/// Activating starts a chat capture for that player only: the next message they send becomes the
/// value and never reaches the chat. Activating again while waiting cancels.
/// </remarks>
public class InputComponent : MenuValueComponent<string>
{
    private int maxLength = 16;

    /// <summary>
    /// Creates an input.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="defaultValue">The value a player starts with.</param>
    /// <param name="validator">Decides whether a typed value is acceptable.</param>
    /// <param name="onChanged">Runs after the value changes.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public InputComponent(
        string text = "",
        string defaultValue = "",
        Func<string, bool>? validator = null,
        Action<MenuValueChangedContext<string>>? onChanged = null,
        string? id = null ) : base(text, id)
    {
        DefaultValue = defaultValue;
        Validator = validator;
        OnChanged = onChanged;
        Comment = "Type your answer in chat";
    }

    /// <summary>
    /// The longest value accepted. Longer input is cut. Never below one.
    /// </summary>
    public int MaxLength {
        get => maxLength;
        set => maxLength = Math.Max(1, value);
    }

    /// <summary>
    /// Decides whether a typed value is acceptable, or null to accept anything non-empty.
    /// </summary>
    public Func<string, bool>? Validator { get; set; }

    /// <summary>
    /// The message sent to the player when the capture starts, or null to send nothing.
    /// </summary>
    public string? HintMessage { get; set; } = "Type your answer in chat.";

    /// <summary>
    /// The text shown while no value has been entered.
    /// </summary>
    public string EmptyText { get; set; } = "(empty)";

    /// <summary>
    /// The text shown while waiting for the player to type.
    /// </summary>
    public string WaitingForInputText { get; set; } = "Waiting for chat input... (select again to cancel)";

    /// <summary>
    /// The text shown briefly after a value is accepted.
    /// </summary>
    public string AcceptedText { get; set; } = "Accepted";

    /// <summary>
    /// The text shown briefly after a value is rejected.
    /// </summary>
    public string InvalidText { get; set; } = "Invalid input";

    /// <summary>
    /// The colour of <see cref="WaitingForInputText"/>.
    /// </summary>
    public string? WaitingForInputColor { get; set; } = "#C0FF3E";

    /// <summary>
    /// The colour of <see cref="AcceptedText"/>.
    /// </summary>
    public string? AcceptedColor { get; set; } = "#00FF00";

    /// <summary>
    /// The colour of <see cref="InvalidText"/>.
    /// </summary>
    public string? InvalidColor { get; set; } = "#FF0000";

    /// <summary>
    /// How long an accepted or invalid status stays on screen, in milliseconds.
    /// </summary>
    public int StatusDurationMs { get; set; } = 2000;

    /// <inheritdoc/>
    public override bool NeedsRedraw( IMenuSession session, DateTime now )
    {
        var state = session.GetState<InputState>(this);

        return base.NeedsRedraw(session, now) || (state.Status is not null && now >= state.StatusUntil);
    }

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        var state = context.Session.GetState<InputState>(this);

        if (state.Status is not null && DateTime.UtcNow >= state.StatusUntil)
        {
            state.Status = null;
            state.StatusColor = null;
        }

        var parts = new List<MenuNode>(2);

        if (RenderLabel(context) is { } label)
        {
            parts.Add(label);
        }

        if (state.Status is not null)
        {
            parts.Add(RenderValue(context, state.Status, state.StatusColor));
        }
        else if (state.Waiting)
        {
            parts.Add(RenderValue(context, WaitingForInputText, WaitingForInputColor));
        }
        else
        {
            var value = GetValue(context.Session);

            parts.Add(string.IsNullOrEmpty(value)
                ? RenderValue(context, EmptyText, DisabledColor)
                : RenderValue(context, value));
        }

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
            var state = ctx.Session.GetState<InputState>(this);

            if (state.Waiting)
            {
                Stop(state);
                ctx.Session.Invalidate();
                return ValueTask.CompletedTask;
            }

            state.Status = null;
            state.Waiting = true;
            state.Capture = ctx.Session.CaptureChat(message => OnChatMessage(ctx.Session, state, message));

            if (!string.IsNullOrEmpty(HintMessage))
            {
                ctx.Player.SendChat(HintMessage);
            }

            ctx.Session.Invalidate();
            return ValueTask.CompletedTask;
        });
    }

    /// <inheritdoc/>
    protected override string Coerce( IMenuSession session, string value )
        => value.Length > MaxLength ? value[..MaxLength] : value;

    private bool OnChatMessage( IMenuSession session, InputState state, string message )
    {
        if (!state.Waiting)
        {
            return false;
        }

        Stop(state);

        var trimmed = message.Trim();
        var accepted = trimmed.Length > 0 && (Validator is null || Validator(Coerce(session, trimmed)));

        if (accepted)
        {
            _ = SetValue(session, trimmed);
        }

        state.Status = accepted ? AcceptedText : InvalidText;
        state.StatusColor = accepted ? AcceptedColor : InvalidColor;
        state.StatusUntil = DateTime.UtcNow.AddMilliseconds(StatusDurationMs);

        session.Invalidate();
        return true;
    }

    private static void Stop( InputState state )
    {
        state.Waiting = false;
        state.Capture?.Dispose();
        state.Capture = null;
    }

    private sealed class InputState
    {
        public IDisposable? Capture { get; set; }

        public bool Waiting { get; set; }

        public string? Status { get; set; }

        public string? StatusColor { get; set; }

        public DateTime StatusUntil { get; set; }
    }
}
