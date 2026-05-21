using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanAcquireItemHook : ICanAcquireItemHook
{
    internal event OnCanAcquireItemPreDelegate? _Pre;
    internal event OnCanAcquireItemPostDelegate? _Post;

    public event OnCanAcquireItemPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanAcquire);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        }
    }

    public event OnCanAcquireItemPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanAcquire);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        }
    }

    public void InvokePre( ref CanAcquireItemPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CanAcquireItemPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
    }
}
