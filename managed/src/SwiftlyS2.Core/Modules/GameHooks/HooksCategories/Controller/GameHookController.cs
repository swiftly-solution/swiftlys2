using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookController( GameHooksService owner ) : IGameHookController
{
    internal readonly ProcessUsercmdsHook ProcessUsercmdsHook = new(owner);
    internal readonly SimulateUserCommandsHook SimulateUserCommandsHook = new(owner);

    public IProcessUsercmdsHook ProcessUsercmds => ProcessUsercmdsHook;
    public ISimulateUserCommandsHook SimulateUserCommands => SimulateUserCommandsHook;
}
