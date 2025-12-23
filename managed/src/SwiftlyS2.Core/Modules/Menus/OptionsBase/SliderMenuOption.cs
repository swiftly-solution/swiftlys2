using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a slider menu option that allows selecting a numeric value within a range.
/// </summary>
public sealed class SliderMenuOption : MenuOptionBase
{
    private readonly ConcurrentDictionary<int, float> values = new();
    private readonly float defaultValue;
    private readonly int totalBars;

    /// <summary>
    /// Gets the minimum value of the slider.
    /// </summary>
    public float Min { get; private init; }

    /// <summary>
    /// Gets the maximum value of the slider.
    /// </summary>
    public float Max { get; private init; }

    /// <summary>
    /// Gets the step increment/decrement value.
    /// </summary>
    public float Step { get; private init; }

    /// <summary>
    /// Occurs when the slider value changes for a player.
    /// </summary>
    public event EventHandler<MenuOptionValueChangedEventArgs<float>>? ValueChanged;

    /// <summary>
    /// Creates an instance of <see cref="SliderMenuOption"/>.
    /// </summary>
    /// <param name="min">The minimum value. Defaults to 0.</param>
    /// <param name="max">The maximum value. Defaults to 100.</param>
    /// <param name="defaultValue">The default starting value. Defaults to the minimum value.</param>
    /// <param name="step">The increment/decrement step. Defaults to 5.</param>
    /// <param name="totalBars">The number of visual bars to display. Defaults to 10.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public SliderMenuOption(
        float min = 0f,
        float max = 100f,
        float? defaultValue = null,
        float step = 5f,
        int totalBars = 10,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        if (min >= max)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"Min ({min}) must be less than Max ({max}). Swapping values.", nameof(min)));
            (min, max) = (max, min);
        }

        if (step <= 0)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(step), $"Step must be greater than 0. Value {step} clamped to 0.1."));
            step = 1f;
        }

        if (totalBars <= 0)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(totalBars), $"Total bars must be greater than 0. Value {totalBars} clamped to 10."));
            totalBars = 10;
        }

        PlaySound = true;
        this.Min = min;
        this.Max = max;
        this.Step = step;
        this.defaultValue = Math.Clamp(defaultValue ?? min, min, max);
        this.totalBars = totalBars;

        values.Clear();
        Click += OnSliderClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="SliderMenuOption"/>.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="min">The minimum value. Defaults to 0.</param>
    /// <param name="max">The maximum value. Defaults to 100.</param>
    /// <param name="defaultValue">The default starting value. Defaults to the minimum value.</param>
    /// <param name="step">The increment/decrement step. Defaults to 5.</param>
    /// <param name="totalBars">The number of visual bars to display. Defaults to 10.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public SliderMenuOption(
        string text,
        float min = 0f,
        float max = 100f,
        float? defaultValue = null,
        float step = 5f,
        int totalBars = 10,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(min, max, defaultValue, step, totalBars, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        var text = base.GetDisplayText(player, displayLine);
        var value = values.GetOrAdd(player.PlayerID, defaultValue);
        var percentage = (value - Min) / (Max - Min);
        var filledBars = (int)(percentage * totalBars);

        var bars = string.Concat(
            Enumerable.Range(0, totalBars).Select(i => i < filledBars ? "<font color='#FFFFFF'>■</font>" : $"<font color='{Menu?.Configuration.DisabledColor ?? "#666666"}'>□</font>")
        );

        var slider = $"<font color='#FFFFFF'>(</font>{bars}<font color='#FF3333'>)</font> <font color='#FFFFFF'>{value:F1}</font>";

        return $"{text}: {slider}";
    }

    /// <summary>
    /// Gets the current slider value for the specified player.
    /// </summary>
    /// <param name="player">The player whose value to retrieve.</param>
    /// <returns>The current slider value.</returns>
    public float GetValue( IPlayer player )
    {
        return values.GetOrAdd(player.PlayerID, defaultValue);
    }

    /// <summary>
    /// Sets the slider value for the specified player.
    /// </summary>
    /// <param name="player">The player whose value to set.</param>
    /// <param name="value">The value to set. Will be clamped between Min and Max.</param>
    public void SetValue( IPlayer player, float value )
    {
        var clampedValue = Math.Clamp(value, Min, Max);
        _ = values.AddOrUpdate(player.PlayerID, clampedValue, ( _, _ ) => clampedValue);
    }

    private ValueTask OnSliderClick( object? sender, MenuOptionClickEventArgs args )
    {
        var oldValue = values.GetOrAdd(args.Player.PlayerID, defaultValue);
        var newValue = Math.Clamp(oldValue + Step > Max ? Min : oldValue + Step, Min, Max);

        _ = values.AddOrUpdate(args.Player.PlayerID, newValue, ( _, _ ) => newValue);

        try
        {
            ValueChanged?.Invoke(this, new MenuOptionValueChangedEventArgs<float> {
                Player = args.Player,
                Option = this,
                OldValue = oldValue,
                NewValue = newValue
            });
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return ValueTask.CompletedTask;
            AnsiConsole.WriteException(e);
        }

        return ValueTask.CompletedTask;
    }
}