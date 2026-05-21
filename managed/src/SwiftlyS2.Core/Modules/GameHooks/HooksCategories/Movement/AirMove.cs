using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirMoveMovementHook : IAirMoveMovementHook
{
    internal event OnAirMoveMovementPreDelegate? _Pre;
    internal event OnAirMoveMovementPostDelegate? _Post;

    public event OnAirMoveMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.AirMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        }
    }

    public event OnAirMoveMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.AirMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        }
    }

    public void InvokePre( ref AirMoveMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref AirMoveMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
    }
}
