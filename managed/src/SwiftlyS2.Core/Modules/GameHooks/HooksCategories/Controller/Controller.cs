using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookController : IGameHookController
{
    internal readonly ProcessUsercmdsEvents ProcessUsercmdsEvents = new();
    internal readonly SimulateUserCommandsEvents SimulateUserCommandsEvents = new();

    public IProcessUsercmdsEvents ProcessUsercmds => ProcessUsercmdsEvents;
    public ISimulateUserCommandsEvents SimulateUserCommands => SimulateUserCommandsEvents;
}
