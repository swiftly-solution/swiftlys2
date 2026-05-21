using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FrictionMovementHook : IFrictionMovementHook
{
    internal event OnFrictionMovementPreDelegate? _Pre;
    internal event OnFrictionMovementPostDelegate? _Post;

    public event OnFrictionMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Friction);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        }
    }

    public event OnFrictionMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Friction);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        }
    }

    public void InvokePre( ref FrictionMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref FrictionMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
    }
}
