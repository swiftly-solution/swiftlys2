using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Engine;

internal class CommandTrackerService : IDisposable
{
    private CommandTrackerManager CommandTrackedManager { get; init; }
    private ISwiftlyCore Core { get; init; }


    public CommandTrackerService(ISwiftlyCore core, CommandTrackerManager commandTrackedManager)
    {
        CommandTrackedManager = commandTrackedManager;
        Core = core;
        Core.Event.OnCommandExecuteHook += CommandTrackedManager.ProcessCommand;
        Core.Event.OnConsoleOutput += CommandTrackedManager.ProcessOutput;
    }

    public void Dispose()
    {
        Core.Event.OnCommandExecuteHook -= CommandTrackedManager.ProcessCommand;
        Core.Event.OnConsoleOutput -= CommandTrackedManager.ProcessOutput;
    }
}