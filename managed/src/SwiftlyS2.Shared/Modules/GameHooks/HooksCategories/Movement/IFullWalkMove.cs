using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct FullWalkMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required bool Ground { get; set; }
}

public ref struct FullWalkMoveMovementPreContext
{
    public FullWalkMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct FullWalkMoveMovementPostContext
{
    public FullWalkMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnFullWalkMoveMovementPreDelegate( ref FullWalkMoveMovementPreContext ctx );
public delegate void OnFullWalkMoveMovementPostDelegate( ref FullWalkMoveMovementPostContext ctx );

public interface IFullWalkMoveMovementHook
{
    public event OnFullWalkMoveMovementPreDelegate Pre;
    public event OnFullWalkMoveMovementPostDelegate Post;
}
