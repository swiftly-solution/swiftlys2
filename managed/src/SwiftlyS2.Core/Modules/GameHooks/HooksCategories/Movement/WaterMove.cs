using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WaterMoveMovementHook : IWaterMoveMovementHook
{
    internal event OnWaterMoveMovementPreDelegate? _Pre;
    internal event OnWaterMoveMovementPostDelegate? _Post;

    public event OnWaterMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        }
    }

    public event OnWaterMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        }
    }

    public void InvokePre( ref WaterMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref WaterMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
    }
}
