using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckFallingMovementData : ICheckFallingMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckFallingMovementEvents : ICheckFallingMovementEvents
{
    internal event OnCheckFallingMovementDelegate? _Pre;
    internal event OnCheckFallingMovementDelegate? _Post;

    public event OnCheckFallingMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckFalling);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        }
    }

    public event OnCheckFallingMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckFalling);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        }
    }

    public void InvokePre( ref ICheckFallingMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckFallingMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
    }
}
