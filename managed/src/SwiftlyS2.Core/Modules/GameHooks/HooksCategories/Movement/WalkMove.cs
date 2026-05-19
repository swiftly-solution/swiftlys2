using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WalkMoveMovementData : IWalkMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class WalkMoveMovementEvents : IWalkMoveMovementEvents
{
    internal event OnWalkMoveMovementDelegate? _Pre;
    internal event OnWalkMoveMovementDelegate? _Post;

    public event OnWalkMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        }
    }

    public event OnWalkMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        }
    }

    public void InvokePre( ref IWalkMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IWalkMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
    }
}
