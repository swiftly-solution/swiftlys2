using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class RunCommandMovementData : IRunCommandMovement
{
    public required IPlayer Player { get; set; }
    public required IUserCmd UserCmd { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class RunCommandMovementEvents : IRunCommandMovementEvents
{
    internal event OnRunCommandMovementDelegate? _Pre;
    internal event OnRunCommandMovementDelegate? _Post;

    public event OnRunCommandMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        }
    }

    public event OnRunCommandMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        }
    }

    public void InvokePre( ref IRunCommandMovement data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref IRunCommandMovement data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
    }
}
