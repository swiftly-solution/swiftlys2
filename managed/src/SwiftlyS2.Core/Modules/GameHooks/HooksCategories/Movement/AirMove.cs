using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirMoveMovementData : IAirMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class AirMoveMovementEvents : IAirMoveMovementEvents
{
    internal event OnAirMoveMovementDelegate? _Pre;
    internal event OnAirMoveMovementDelegate? _Post;

    public event OnAirMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.AirMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        }
    }

    public event OnAirMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.AirMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        }
    }

    public void InvokePre( ref IAirMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IAirMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.AirMove);
    }
}
