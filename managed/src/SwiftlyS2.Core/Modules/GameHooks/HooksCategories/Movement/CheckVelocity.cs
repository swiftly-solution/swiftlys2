using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckVelocityMovementData : ICheckVelocityMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckVelocityMovementEvents : ICheckVelocityMovementEvents
{
    internal event OnCheckVelocityMovementDelegate? _Pre;
    internal event OnCheckVelocityMovementDelegate? _Post;

    public event OnCheckVelocityMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckVelocity);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity);
        }
    }

    public event OnCheckVelocityMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckVelocity);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity);
        }
    }

    public void InvokePre( ref ICheckVelocityMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckVelocityMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity);
    }
}
