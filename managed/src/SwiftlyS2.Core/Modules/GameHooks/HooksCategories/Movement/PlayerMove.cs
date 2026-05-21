using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PlayerMoveMovementHook : IPlayerMoveMovementHook
{
    internal event OnPlayerMoveMovementPreDelegate? _Pre;
    internal event OnPlayerMoveMovementPostDelegate? _Post;

    public event OnPlayerMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.PlayerMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        }
    }

    public event OnPlayerMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.PlayerMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        }
    }

    public void InvokePre( ref PlayerMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref PlayerMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
    }
}
