using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct LadderMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct LadderMoveMovementPreContext
{
    public LadderMoveMovementParams Params;
    private bool _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( bool result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal bool Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct LadderMoveMovementPostContext
{
    public LadderMoveMovementParams Params;
    public bool Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnLadderMoveMovementPreDelegate( ref LadderMoveMovementPreContext ctx );
public delegate void OnLadderMoveMovementPostDelegate( ref LadderMoveMovementPostContext ctx );

public interface ILadderMoveMovementHook
{
    public event OnLadderMoveMovementPreDelegate Pre;
    public event OnLadderMoveMovementPostDelegate Post;
}
