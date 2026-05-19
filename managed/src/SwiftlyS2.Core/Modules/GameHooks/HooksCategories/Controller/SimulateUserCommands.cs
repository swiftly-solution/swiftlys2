using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SimulateUserCommands : ISimulateUserCommandsController
{
    public required IPlayer Player { get; set; }

    public required HookResult Result { get; set; } = HookResult.Continue;

}

internal sealed class SimulateUserCommandsEvents : ISimulateUserCommandsEvents
{
    private event OnSimulateUserCommandsDelegate? _Pre;
    private event OnSimulateUserCommandsDelegate? _Post;

    public event OnSimulateUserCommandsDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.SimulateUserCommands);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands);
        }
    }

    public event OnSimulateUserCommandsDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.SimulateUserCommands);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands);
        }
    }

    public void InvokePre( ref ISimulateUserCommandsController data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref ISimulateUserCommandsController data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands);
    }
}
