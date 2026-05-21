using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessMovementMovementHook : IProcessMovementMovementHook
{
    internal event OnProcessMovementMovementPreDelegate? _Pre;
    internal event OnProcessMovementMovementPostDelegate? _Post;

    public event OnProcessMovementMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        }
    }

    public event OnProcessMovementMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        }
    }

    public void InvokePre( ref ProcessMovementMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref ProcessMovementMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement);
    }
}
