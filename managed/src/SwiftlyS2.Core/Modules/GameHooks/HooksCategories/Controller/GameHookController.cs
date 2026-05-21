using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookController : IGameHookController
{
    internal readonly ProcessUsercmdsHook ProcessUsercmdsHook = new();
    internal readonly SimulateUserCommandsHook SimulateUserCommandsHook = new();

    public IProcessUsercmdsHook ProcessUsercmds => ProcessUsercmdsHook;
    public ISimulateUserCommandsHook SimulateUserCommands => SimulateUserCommandsHook;
}
