using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct WaterMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct WaterMoveMovementPreContext
{
    public WaterMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct WaterMoveMovementPostContext
{
    public WaterMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnWaterMoveMovementPreDelegate( ref WaterMoveMovementPreContext ctx );
public delegate void OnWaterMoveMovementPostDelegate( ref WaterMoveMovementPostContext ctx );

public interface IWaterMoveMovementHook
{
    public event OnWaterMoveMovementPreDelegate Pre;
    public event OnWaterMoveMovementPostDelegate Post;
}
