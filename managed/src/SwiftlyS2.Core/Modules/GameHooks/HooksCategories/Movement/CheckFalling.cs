using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckFallingMovementHook : ICheckFallingMovementHook
{
    internal event OnCheckFallingMovementPreDelegate? _Pre;
    internal event OnCheckFallingMovementPostDelegate? _Post;

    public event OnCheckFallingMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckFalling);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        }
    }

    public event OnCheckFallingMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckFalling);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        }
    }

    public void InvokePre( ref CheckFallingMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CheckFallingMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckFalling);
    }
}
