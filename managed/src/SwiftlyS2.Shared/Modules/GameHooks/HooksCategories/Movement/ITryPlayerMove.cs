using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Shared.GameHooks;

public interface ITryPlayerMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    /// <summary>
    /// The first destination vector. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public Vector FirstDest { get; set; }
    /// <summary>
    /// The first trace result. Read-only snapshot of the native CGameTrace.
    /// </summary>
    public TraceResult FirstTrace { get; }
    /// <summary>
    /// Whether the player is surfing. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public bool IsSurfing { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnTryPlayerMoveMovementDelegate( ref ITryPlayerMoveMovement data );

public interface ITryPlayerMoveMovementEvents
{
    public event OnTryPlayerMoveMovementDelegate Pre;
    public event OnTryPlayerMoveMovementDelegate Post;
}
