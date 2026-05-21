using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CheckVelocityMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CheckVelocityMovementPreContext
{
    public CheckVelocityMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CheckVelocityMovementPostContext
{
    public CheckVelocityMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCheckVelocityMovementPreDelegate( ref CheckVelocityMovementPreContext ctx );
public delegate void OnCheckVelocityMovementPostDelegate( ref CheckVelocityMovementPostContext ctx );

public interface ICheckVelocityMovementHook
{
    public event OnCheckVelocityMovementPreDelegate Pre;
    public event OnCheckVelocityMovementPostDelegate Post;
}
