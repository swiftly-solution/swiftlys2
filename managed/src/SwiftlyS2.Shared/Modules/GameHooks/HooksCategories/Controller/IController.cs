namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookController
{
    /// <summary>
    /// Hook triggered when processing user commands.
    /// </summary>
    public IProcessUsercmdsHook ProcessUsercmds { get; }

    /// <summary>
    /// Hook triggered when simulating user commands.
    /// </summary>
    public ISimulateUserCommandsHook SimulateUserCommands { get; }
}
