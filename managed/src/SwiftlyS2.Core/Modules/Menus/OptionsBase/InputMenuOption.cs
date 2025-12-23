using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a menu option that allows text input from players.
/// </summary>
public sealed class InputMenuOption : MenuOptionBase
{
    private readonly ConcurrentDictionary<int, string> values = new();
    private readonly ConcurrentDictionary<int, bool> waitingForInput = new();
    private readonly ConcurrentDictionary<int, string> inputStates = new();
    private readonly ConcurrentDictionary<int, CancellationTokenSource> statusClearTasks = new();
    private readonly string defaultValue;
    private readonly string hintMessage;
    private readonly Func<string, bool>? validator;
    private readonly int maxLength;
    private Guid chatHookGuid;

    /// <summary>
    /// Occurs when a player submits a valid input value.
    /// </summary>
    public event EventHandler<MenuOptionValueChangedEventArgs<string>>? ValueChanged;

    /// <summary>
    /// Creates an instance of <see cref="InputMenuOption"/>.
    /// </summary>
    /// <param name="defaultValue">The default input value. Defaults to empty string.</param>
    /// <param name="maxLength">Maximum input length. Defaults to 16.</param>
    /// <param name="hintMessage">Optional hint message to display when waiting for input. Defaults to English prompt.</param>
    /// <param name="validator">Optional function to validate input. Returns true if valid.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public InputMenuOption(
        int maxLength = 16,
        Func<string, bool>? validator = null,
        string defaultValue = "",
        string? hintMessage = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        if (maxLength <= 0)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(maxLength), $"Max length must be greater than 0. Value {maxLength} clamped to 16."));
            maxLength = 16;
        }

        PlaySound = true;
        this.maxLength = maxLength;
        this.validator = validator;
        this.defaultValue = defaultValue;
        this.hintMessage = hintMessage ?? $"Please type your input (max {maxLength} characters)";

        values.Clear();
        Click += OnInputClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="InputMenuOption"/>.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="maxLength">Maximum input length. Defaults to 16.</param>
    /// <param name="validator">Optional function to validate input. Returns true if valid.</param>
    /// <param name="defaultValue">The default input value. Defaults to empty string.</param>
    /// <param name="hintMessage">Optional hint message to display when waiting for input. Defaults to English prompt.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public InputMenuOption(
        string text,
        int maxLength = 16,
        Func<string, bool>? validator = null,
        string defaultValue = "",
        string? hintMessage = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(maxLength, validator, defaultValue, hintMessage, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        if (inputStates.TryGetValue(player.PlayerID, out var state))
        {
            return state;
        }

        var text = base.GetDisplayText(player, displayLine);
        var value = values.GetOrAdd(player.PlayerID, defaultValue);
        var displayValue = string.IsNullOrEmpty(value) ? $"<font color='{Menu?.Configuration.DisabledColor ?? "#666666"}'>(empty)</font>" : $"<font color='#FFFFFF'>{value}</font>";
        return $"{text}: {displayValue}";
    }

    /// <summary>
    /// Gets the current input value for the specified player.
    /// </summary>
    /// <param name="player">The player whose value to retrieve.</param>
    /// <returns>The current input value.</returns>
    public string GetValue( IPlayer player )
    {
        return values.GetOrAdd(player.PlayerID, defaultValue);
    }

    /// <summary>
    /// Sets the input value for the specified player and triggers validation.
    /// </summary>
    /// <param name="player">The player whose value to set.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>True if the value is valid and was set, false otherwise.</returns>
    public bool SetValue( IPlayer player, string value )
    {
        if (value.Length > maxLength)
        {
            value = value[..maxLength];
        }

        if (validator != null && !validator(value))
        {
            return false;
        }

        var oldValue = values.GetOrAdd(player.PlayerID, defaultValue);
        _ = values.AddOrUpdate(player.PlayerID, value, ( _, _ ) => value);

        try
        {
            ValueChanged?.Invoke(this, new MenuOptionValueChangedEventArgs<string> {
                Player = player,
                Option = this,
                OldValue = oldValue,
                NewValue = value
            });
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return false;
            AnsiConsole.WriteException(e);
        }

        return true;
    }

    private ValueTask OnInputClick( object? sender, MenuOptionClickEventArgs args )
    {
        if (Menu?.MenuManager.Core != null && chatHookGuid == Guid.Empty)
        {
            chatHookGuid = Menu.MenuManager.Core.Command.HookClientChat(OnChatInput);
        }

        if (statusClearTasks.TryGetValue(args.Player.PlayerID, out var oldCts))
        {
            oldCts.Cancel();
            oldCts.Dispose();
            _ = statusClearTasks.TryRemove(args.Player.PlayerID, out _);
        }

        if (waitingForInput.ContainsKey(args.Player.PlayerID))
        {
            _ = waitingForInput.TryRemove(args.Player.PlayerID, out _);
            _ = inputStates.TryRemove(args.Player.PlayerID, out _);
            return ValueTask.CompletedTask;
        }

        _ = inputStates.AddOrUpdate(args.Player.PlayerID, $"<font color='#C0FF3E'>Waiting</font> (click again to cancel)", ( _, _ ) => $"<font color='#C0FF3E'>Waiting</font> (click again to cancel)");
        args.Player.SendMessageAsync(MessageType.Chat, hintMessage);

        _ = waitingForInput.AddOrUpdate(args.Player.PlayerID, true, ( _, _ ) => true);

        return ValueTask.CompletedTask;
    }

    private HookResult OnChatInput( int playerId, string text, bool teamonly )
    {
        var player = Menu?.MenuManager.Core.PlayerManager.GetPlayer(playerId);
        if (player == null || !waitingForInput.ContainsKey(player.PlayerID))
        {
            return HookResult.Continue;
        }

        var input = text.Trim();

        _ = waitingForInput.TryRemove(player.PlayerID, out _);

        var statusMessage = string.IsNullOrWhiteSpace(input) || !SetValue(player, input) ? "<font color='#FF0000'>Invalid input</font>" : $"<font color='#00FF00'>Accepted</font>";

        _ = inputStates.AddOrUpdate(player.PlayerID, statusMessage, ( _, _ ) => statusMessage);

        if (statusClearTasks.TryGetValue(player.PlayerID, out var oldCts))
        {
            oldCts.Cancel();
            oldCts.Dispose();
        }

        var cts = new CancellationTokenSource();
        _ = statusClearTasks.AddOrUpdate(player.PlayerID, cts, ( _, _ ) => cts);

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(2000, cts.Token);
                _ = inputStates.TryRemove(player.PlayerID, out _);
                if (statusClearTasks.TryRemove(player.PlayerID, out var completedCts))
                {
                    completedCts.Dispose();
                }
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return;
            }
        }, cts.Token);

        return HookResult.Stop;
    }

    public override void Dispose()
    {
        foreach (var kvp in statusClearTasks)
        {
            kvp.Value.Cancel();
            kvp.Value.Dispose();
        }
        statusClearTasks.Clear();

        if (Menu?.MenuManager.Core != null && chatHookGuid != Guid.Empty)
        {
            Menu.MenuManager.Core.Command.UnhookClientChat(chatHookGuid);
            chatHookGuid = Guid.Empty;
        }

        base.Dispose();
    }
}