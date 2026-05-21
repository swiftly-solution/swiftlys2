using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckWaterMovementHook : ICheckWaterMovementHook
{
    internal event OnCheckWaterMovementPreDelegate? _Pre;
    internal event OnCheckWaterMovementPostDelegate? _Post;

    public event OnCheckWaterMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        }
    }

    public event OnCheckWaterMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        }
    }

    public void InvokePre( ref CheckWaterMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CheckWaterMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
    }
}
