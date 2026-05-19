using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FullWalkMoveMovementData : IFullWalkMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public bool Ground { get; set; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class FullWalkMoveMovementEvents : IFullWalkMoveMovementEvents
{
    internal event OnFullWalkMoveMovementDelegate? _Pre;
    internal event OnFullWalkMoveMovementDelegate? _Post;

    public event OnFullWalkMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.FullWalkMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove);
        }
    }

    public event OnFullWalkMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.FullWalkMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove);
        }
    }

    public void InvokePre( ref IFullWalkMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IFullWalkMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove);
    }
}
