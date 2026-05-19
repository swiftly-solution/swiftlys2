using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessMovementMovementData : IProcessMovementMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class ProcessMovementMovementEvents : IProcessMovementMovementEvents
{
    internal event OnProcessMovementMovementDelegate? _Pre;
    internal event OnProcessMovementMovementDelegate? _Post;

    public event OnProcessMovementMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        }
    }

    public event OnProcessMovementMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        }
    }

    public void InvokePre( ref IProcessMovementMovement data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref IProcessMovementMovement data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
    }
}
