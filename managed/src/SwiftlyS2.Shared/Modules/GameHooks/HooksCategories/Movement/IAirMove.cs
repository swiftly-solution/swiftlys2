using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct AirMoveMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct AirMoveMovementPreContext
{
    public AirMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct AirMoveMovementPostContext
{
    public AirMoveMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnAirMoveMovementPreDelegate( ref AirMoveMovementPreContext ctx );
public delegate void OnAirMoveMovementPostDelegate( ref AirMoveMovementPostContext ctx );

public interface IAirMoveMovementHook
{
    public event OnAirMoveMovementPreDelegate Pre;
    public event OnAirMoveMovementPostDelegate Post;
}
