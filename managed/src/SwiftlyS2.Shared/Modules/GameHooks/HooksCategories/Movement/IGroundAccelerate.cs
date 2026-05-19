using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IGroundAccelerateMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    /// <summary>
    /// The wish direction vector. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public Vector WishDirection { get; set; }
    /// <summary>
    /// The frame time. Read-only.
    /// </summary>
    public float FrameTime { get; }
    /// <summary>
    /// The wish speed. Modifications are passed to the original function.
    /// </summary>
    public float WishSpeed { get; set; }
    /// <summary>
    /// The acceleration value. Modifications are passed to the original function.
    /// </summary>
    public float Acceleration { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnGroundAccelerateMovementDelegate( ref IGroundAccelerateMovement data );

public interface IGroundAccelerateMovementEvents
{
    public event OnGroundAccelerateMovementDelegate Pre;
    public event OnGroundAccelerateMovementDelegate Post;
}
