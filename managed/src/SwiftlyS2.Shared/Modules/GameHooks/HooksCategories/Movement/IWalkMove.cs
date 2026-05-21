using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct WalkMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct WalkMoveMovementPreContext
{
    public WalkMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct WalkMoveMovementPostContext
{
    public WalkMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnWalkMoveMovementPreDelegate( ref WalkMoveMovementPreContext ctx );
public delegate void OnWalkMoveMovementPostDelegate( ref WalkMoveMovementPostContext ctx );

public interface IWalkMoveMovementHook
{
    public event OnWalkMoveMovementPreDelegate Pre;
    public event OnWalkMoveMovementPostDelegate Post;
}
