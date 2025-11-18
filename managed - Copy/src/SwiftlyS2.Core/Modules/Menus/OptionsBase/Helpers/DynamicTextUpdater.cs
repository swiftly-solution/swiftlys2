using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus.OptionsBase.Helpers;

internal sealed class DynamicTextUpdater : IDisposable
{
    private readonly TextStyleProcessor processor;
    private readonly Func<string> getSourceText;
    private readonly Func<MenuOptionTextStyle> getTextStyle;
    private readonly Func<float> getMaxWidth;
    private readonly Action<string> setDynamicText;
    private readonly CancellationTokenSource cancellationTokenSource;

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

        this.getSourceText = getSourceText;
        this.getTextStyle = getTextStyle;
        this.getMaxWidth = getMaxWidth;
        this.setDynamicText = setDynamicText;

        processor = new();
        cancellationTokenSource = new();
        _ = Task.Run(() => UpdateLoopAsync(updateIntervalMs, pauseIntervalMs, cancellationTokenSource.Token), cancellationTokenSource.Token);
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
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();

        processor.Dispose();

        disposed = true;
        GC.SuppressFinalize(this);
    }

    private async Task UpdateLoopAsync( int intervalMs, int pauseIntervalMs, CancellationToken token )
    {
        while (!token.IsCancellationRequested && !disposed)
        {
            try
            {
                await Task.Delay(intervalMs, token);
                var sourceText = getSourceText();
                var textStyle = getTextStyle();
                var maxWidth = getMaxWidth();
                var (styledText, offset) = processor.ApplyHorizontalStyle(sourceText, textStyle, maxWidth);
                setDynamicText(styledText);
                // Console.WriteLine($"sourceText: {sourceText}, textStyle: {textStyle}, maxWidth: {maxWidth}, styledText: {styledText}, offset: {offset}");

                if (offset == 0)
                {
                    await Task.Delay(pauseIntervalMs, token);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch
            {
            }
        }
    }
}