using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WalkMoveMovementHook : IWalkMoveMovementHook
{
    internal event OnWalkMoveMovementPreDelegate? _Pre;
    internal event OnWalkMoveMovementPostDelegate? _Post;

    public event OnWalkMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        }
    }

    public event OnWalkMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        }
    }

    public void InvokePre( ref WalkMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref WalkMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove);
    }
}
