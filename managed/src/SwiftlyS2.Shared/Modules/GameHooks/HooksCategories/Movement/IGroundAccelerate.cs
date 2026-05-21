using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct GroundAccelerateMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    /// <summary>
    /// The wish direction vector. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public required Vector WishDirection { get; set; }
    /// <summary>
    /// The frame time. Read-only.
    /// </summary>
    public required float FrameTime { get; init; }
    /// <summary>
    /// The wish speed. Modifications are passed to the original function.
    /// </summary>
    public required float WishSpeed { get; set; }
    /// <summary>
    /// The acceleration value. Modifications are passed to the original function.
    /// </summary>
    public required float Acceleration { get; set; }
}

public ref struct GroundAccelerateMovementPreContext
{
    public GroundAccelerateMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct GroundAccelerateMovementPostContext
{
    public GroundAccelerateMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnGroundAccelerateMovementPreDelegate( ref GroundAccelerateMovementPreContext ctx );
public delegate void OnGroundAccelerateMovementPostDelegate( ref GroundAccelerateMovementPostContext ctx );

public interface IGroundAccelerateMovementHook
{
    public event OnGroundAccelerateMovementPreDelegate Pre;
    public event OnGroundAccelerateMovementPostDelegate Post;
}
