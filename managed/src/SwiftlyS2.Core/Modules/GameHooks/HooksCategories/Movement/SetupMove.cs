using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SetupMoveMovementHook : ISetupMoveMovementHook
{
    internal event OnSetupMoveMovementPreDelegate? _Pre;
    internal event OnSetupMoveMovementPostDelegate? _Post;

    public event OnSetupMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        }
    }

    public event OnSetupMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        }
    }

    public void InvokePre( ref SetupMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref SetupMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
    }
}
