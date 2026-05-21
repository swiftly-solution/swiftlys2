using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonModernMovementHook : ICheckJumpButtonModernMovementHook
{
    internal event OnCheckJumpButtonModernMovementPreDelegate? _Pre;
    internal event OnCheckJumpButtonModernMovementPostDelegate? _Post;

    public event OnCheckJumpButtonModernMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        }
    }

    public event OnCheckJumpButtonModernMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        }
    }

    public void InvokePre( ref CheckJumpButtonModernMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CheckJumpButtonModernMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
    }
}
