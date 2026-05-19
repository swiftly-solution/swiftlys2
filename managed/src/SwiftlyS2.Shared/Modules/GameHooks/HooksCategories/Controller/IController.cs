namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookController
{
    /// <summary>
    /// Hooks related to processing user commands.
    /// </summary>
    public IProcessUsercmdsEvents ProcessUsercmds { get; }

    /// <summary>
    /// Hooks related to simulating the user commands, or processing the movement (?)
    /// </summary>
    public ISimulateUserCommandsEvents SimulateUserCommands { get; }
}
