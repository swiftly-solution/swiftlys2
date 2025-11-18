using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.ConsoleOutput;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.ConsoleOutput;

internal class ConsoleOutputService : IConsoleOutputService, IDisposable
{
    public ConsoleOutputService()
    {
    }

    public bool IsFilterEnabled()
    {
        return NativeConsoleOutput.IsEnabled();
    }

    public void ToggleFilter()
    {
        NativeConsoleOutput.ToggleFilter();
    }

    public void ReloadFilterConfiguration()
    {
        NativeConsoleOutput.ReloadFilterConfiguration();
    }

    public bool NeedsFiltering(string message)
    {
        return NativeConsoleOutput.NeedsFiltering(message);
    }

    public string GetCounterText()
    {
        return NativeConsoleOutput.GetCounterText();
    }

    public void WriteToServerConsole(string message)
    {
        NativeEngineHelpers.SendMessageToConsole(message);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}