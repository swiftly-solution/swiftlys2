using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IAirAccelerateMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    /// <summary>
    /// The wish direction vector. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public Vector WishDirection { get; set; }
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

public delegate void OnAirAccelerateMovementDelegate( ref IAirAccelerateMovement data );

public interface IAirAccelerateMovementEvents
{
    public event OnAirAccelerateMovementDelegate Pre;
    public event OnAirAccelerateMovementDelegate Post;
}
