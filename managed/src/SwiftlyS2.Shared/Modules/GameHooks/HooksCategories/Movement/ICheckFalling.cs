using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CheckFallingMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CheckFallingMovementPreContext
{
    public CheckFallingMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CheckFallingMovementPostContext
{
    public CheckFallingMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCheckFallingMovementPreDelegate( ref CheckFallingMovementPreContext ctx );
public delegate void OnCheckFallingMovementPostDelegate( ref CheckFallingMovementPostContext ctx );

public interface ICheckFallingMovementHook
{
    public event OnCheckFallingMovementPreDelegate Pre;
    public event OnCheckFallingMovementPostDelegate Post;
}
