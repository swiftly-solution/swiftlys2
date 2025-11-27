using Spectre.Console;
using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus.OptionsBase.Helpers;

internal sealed class DynamicTextUpdater : IDisposable
{
    private readonly TextStyleProcessor processor;
    private readonly Func<string> getSourceText;
    private readonly Func<MenuOptionTextStyle> getTextStyle;
    private readonly Func<float> getMaxWidth;
    private readonly Action<string> setDynamicText;
    private readonly int updateIntervalMs;
    private readonly int pauseIntervalMs;
    private volatile bool isPaused;
    private DateTime lastUpdateTime = DateTime.MinValue;
    private DateTime pauseEndTime = DateTime.MinValue;

    private volatile bool disposed;

    public DynamicTextUpdater(
        Func<string> getSourceText,
        Func<MenuOptionTextStyle> getTextStyle,
        Func<float> getMaxWidth,
        Action<string> setDynamicText,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 )
    {
        disposed = false;
        isPaused = true;

        this.getSourceText = getSourceText;
        this.getTextStyle = getTextStyle;
        this.getMaxWidth = getMaxWidth;
        this.setDynamicText = setDynamicText;
        this.updateIntervalMs = updateIntervalMs;
        this.pauseIntervalMs = pauseIntervalMs;

        processor = new();
    }

    ~DynamicTextUpdater()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        // Console.WriteLine($"{GetType().Name} has been disposed.");
        disposed = true;

        processor.Dispose();

        GC.SuppressFinalize(this);
    }

    public void Pause()
    {
        if (!disposed)
        {
            isPaused = true;
        }
    }

    public void Resume()
    {
        if (!disposed)
        {
            isPaused = false;
            pauseEndTime = DateTime.MinValue;
        }
    }

    public void TryUpdate( DateTime now )
    {
        if (disposed || isPaused)
        {
            return;
        }

        // Check if still in pause interval
        if (now < pauseEndTime)
        {
            return;
        }

        // Check if enough time has passed since last update
        if (lastUpdateTime != DateTime.MinValue)
        {
            if ((now - lastUpdateTime).TotalMilliseconds < updateIntervalMs)
            {
                return;
            }
        }

        try
        {
            var sourceText = getSourceText();
            var textStyle = getTextStyle();
            var maxWidth = getMaxWidth();
            var (styledText, offset) = processor.ApplyHorizontalStyle(sourceText, textStyle, maxWidth);
            setDynamicText(styledText);

            lastUpdateTime = now;

            // If offset is 0 (text fits completely), enter pause interval
            if (offset == 0)
            {
                pauseEndTime = now.AddMilliseconds(pauseIntervalMs);
            }
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(e))
            {
                AnsiConsole.WriteException(e);
            }
        }
    }
}