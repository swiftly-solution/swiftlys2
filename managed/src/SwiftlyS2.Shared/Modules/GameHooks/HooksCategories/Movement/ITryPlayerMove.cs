using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Shared.GameHooks;

public struct TryPlayerMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    /// <summary>
    /// The first destination vector. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public required Vector FirstDest { get; set; }
    /// <summary>
    /// The first trace result. Read-only snapshot of the native CGameTrace.
    /// </summary>
    public required TraceResult FirstTrace { get; init; }
    /// <summary>
    /// Whether the player is surfing. Wraps a native pointer — modifications are written back to native memory.
    /// </summary>
    public required bool IsSurfing { get; set; }
}

public ref struct TryPlayerMoveMovementPreContext
{
    public TryPlayerMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct TryPlayerMoveMovementPostContext
{
    public TryPlayerMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnTryPlayerMoveMovementPreDelegate( ref TryPlayerMoveMovementPreContext ctx );
public delegate void OnTryPlayerMoveMovementPostDelegate( ref TryPlayerMoveMovementPostContext ctx );

public interface ITryPlayerMoveMovementHook
{
    public event OnTryPlayerMoveMovementPreDelegate Pre;
    public event OnTryPlayerMoveMovementPostDelegate Post;
}
