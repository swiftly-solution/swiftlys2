using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct PlayerMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct PlayerMoveMovementPreContext
{
    public PlayerMoveMovementParams Params;
    private bool _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( bool result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal bool Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct PlayerMoveMovementPostContext
{
    public PlayerMoveMovementParams Params;
    public bool Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnPlayerMoveMovementPreDelegate( ref PlayerMoveMovementPreContext ctx );
public delegate void OnPlayerMoveMovementPostDelegate( ref PlayerMoveMovementPostContext ctx );

public interface IPlayerMoveMovementHook
{
    public event OnPlayerMoveMovementPreDelegate Pre;
    public event OnPlayerMoveMovementPostDelegate Post;
}
