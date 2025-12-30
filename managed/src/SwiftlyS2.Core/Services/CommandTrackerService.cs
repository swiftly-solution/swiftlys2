using SwiftlyS2.Shared;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Engine;

internal class CommandTrackerService : IDisposable
{
    private readonly ISwiftlyCore core;
    private readonly CommandTrackerManager commandTrackedManager;

    public CommandTrackerService( ISwiftlyCore core, CommandTrackerManager commandTrackedManager )
    {
        this.core = core;
        this.commandTrackedManager = commandTrackedManager;
        core.Event.OnCommandExecuteHook += commandTrackedManager.ProcessCommand;
        core.Event.OnConsoleOutput += commandTrackedManager.ProcessOutput;
    }

    public void Dispose()
    {
        core.Event.OnCommandExecuteHook -= commandTrackedManager.ProcessCommand;
        core.Event.OnConsoleOutput -= commandTrackedManager.ProcessOutput;
    }
}