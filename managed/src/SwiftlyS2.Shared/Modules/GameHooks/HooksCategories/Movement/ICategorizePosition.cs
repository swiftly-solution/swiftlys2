using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CategorizePositionMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required bool StayOnGround { get; set; }
}

public ref struct CategorizePositionMovementPreContext
{
    public CategorizePositionMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CategorizePositionMovementPostContext
{
    public CategorizePositionMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCategorizePositionMovementPreDelegate( ref CategorizePositionMovementPreContext ctx );
public delegate void OnCategorizePositionMovementPostDelegate( ref CategorizePositionMovementPostContext ctx );

public interface ICategorizePositionMovementHook
{
    public event OnCategorizePositionMovementPreDelegate Pre;
    public event OnCategorizePositionMovementPostDelegate Post;
}
