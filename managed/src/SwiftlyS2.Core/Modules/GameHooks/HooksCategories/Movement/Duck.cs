using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class DuckMovementHook : IDuckMovementHook
{
    internal event OnDuckMovementPreDelegate? _Pre;
    internal event OnDuckMovementPostDelegate? _Post;

    public event OnDuckMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Duck);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        }
    }

    public event OnDuckMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Duck);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        }
    }

    public void InvokePre( ref DuckMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref DuckMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
    }
}
