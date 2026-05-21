using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TryPlayerMoveMovementHook : ITryPlayerMoveMovementHook
{
    internal event OnTryPlayerMoveMovementPreDelegate? _Pre;
    internal event OnTryPlayerMoveMovementPostDelegate? _Post;

    public event OnTryPlayerMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        }
    }

    public event OnTryPlayerMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        }
    }

    public void InvokePre( ref TryPlayerMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref TryPlayerMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
    }
}
