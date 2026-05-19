using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GroundAccelerateMovementData : IGroundAccelerateMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required nint WishDirectionPtr { get; init; }
    public float FrameTime { get; init; }
    public float WishSpeed { get; set; }
    public float Acceleration { get; set; }
    public HookResult Result { get; set; } = HookResult.Continue;

    public unsafe Vector WishDirection
    {
        get => *(Vector*)WishDirectionPtr;
        set => *(Vector*)WishDirectionPtr = value;
    }
}

internal sealed class GroundAccelerateMovementEvents : IGroundAccelerateMovementEvents
{
    internal event OnGroundAccelerateMovementDelegate? _Pre;
    internal event OnGroundAccelerateMovementDelegate? _Post;

    public event OnGroundAccelerateMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.GroundAccelerate);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate);
        }
    }

    public event OnGroundAccelerateMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.GroundAccelerate);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate);
        }
    }

    public void InvokePre( ref IGroundAccelerateMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IGroundAccelerateMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate);
    }
}
