using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CheckParametersMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CheckParametersMovementPreContext
{
    public CheckParametersMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CheckParametersMovementPostContext
{
    public CheckParametersMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCheckParametersMovementPreDelegate( ref CheckParametersMovementPreContext ctx );
public delegate void OnCheckParametersMovementPostDelegate( ref CheckParametersMovementPostContext ctx );

public interface ICheckParametersMovementHook
{
    public event OnCheckParametersMovementPreDelegate Pre;
    public event OnCheckParametersMovementPostDelegate Post;
}
