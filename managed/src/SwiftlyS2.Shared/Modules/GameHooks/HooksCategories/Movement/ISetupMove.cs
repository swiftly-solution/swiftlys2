using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct SetupMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IUserCmd UserCmd { get; init; }
    public required IMoveData MoveData { get; init; }
}

public ref struct SetupMoveMovementPreContext
{
    public SetupMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct SetupMoveMovementPostContext
{
    public SetupMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnSetupMoveMovementPreDelegate( ref SetupMoveMovementPreContext ctx );
public delegate void OnSetupMoveMovementPostDelegate( ref SetupMoveMovementPostContext ctx );

public interface ISetupMoveMovementHook
{
    public event OnSetupMoveMovementPreDelegate Pre;
    public event OnSetupMoveMovementPostDelegate Post;
}
