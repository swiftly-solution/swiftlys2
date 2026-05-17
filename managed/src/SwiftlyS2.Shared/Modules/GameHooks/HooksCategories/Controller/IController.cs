namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookController
{
    /// <summary>
    /// Hooks related to processing user commands.
    /// </summary>
    public IProcessUsercmdsEvents ProcessUsercmds { get; }
}
