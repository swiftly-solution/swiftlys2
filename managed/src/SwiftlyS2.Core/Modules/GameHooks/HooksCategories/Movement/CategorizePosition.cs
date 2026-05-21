using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CategorizePositionMovementHook : ICategorizePositionMovementHook
{
    internal event OnCategorizePositionMovementPreDelegate? _Pre;
    internal event OnCategorizePositionMovementPostDelegate? _Post;

    public event OnCategorizePositionMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        }
    }

    public event OnCategorizePositionMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        }
    }

    public void InvokePre( ref CategorizePositionMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CategorizePositionMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
    }
}
