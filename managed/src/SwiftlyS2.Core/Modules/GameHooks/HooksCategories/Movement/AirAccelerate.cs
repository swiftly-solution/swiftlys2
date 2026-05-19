using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirAccelerateMovementData : IAirAccelerateMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required nint WishDirectionPtr { get; init; }
    public float WishSpeed { get; set; }
    public float Acceleration { get; set; }
    public HookResult Result { get; set; } = HookResult.Continue;

    public unsafe Vector WishDirection
    {
        get => *(Vector*)WishDirectionPtr;
        set => *(Vector*)WishDirectionPtr = value;
    }
}

internal sealed class AirAccelerateMovementEvents : IAirAccelerateMovementEvents
{
    internal event OnAirAccelerateMovementDelegate? _Pre;
    internal event OnAirAccelerateMovementDelegate? _Post;

    public event OnAirAccelerateMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.AirAccelerate);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate);
        }
    }

    public event OnAirAccelerateMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.AirAccelerate);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate);
        }
    }

    public void InvokePre( ref IAirAccelerateMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IAirAccelerateMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate);
    }
}
