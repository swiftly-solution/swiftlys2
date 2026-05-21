using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpModernMovementHook : IOnJumpModernMovementHook
{
    internal event OnOnJumpModernMovementPreDelegate? _Pre;
    internal event OnOnJumpModernMovementPostDelegate? _Post;

    public event OnOnJumpModernMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        }
    }

    public event OnOnJumpModernMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        }
    }

    public void InvokePre( ref OnJumpModernMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref OnJumpModernMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
    }
}
