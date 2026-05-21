using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct ProcessMovementMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct ProcessMovementMovementPreContext
{
    public ProcessMovementMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct ProcessMovementMovementPostContext
{
    public ProcessMovementMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnProcessMovementMovementPreDelegate( ref ProcessMovementMovementPreContext ctx );
public delegate void OnProcessMovementMovementPostDelegate( ref ProcessMovementMovementPostContext ctx );

public interface IProcessMovementMovementHook
{
    public event OnProcessMovementMovementPreDelegate Pre;
    public event OnProcessMovementMovementPostDelegate Post;
}
