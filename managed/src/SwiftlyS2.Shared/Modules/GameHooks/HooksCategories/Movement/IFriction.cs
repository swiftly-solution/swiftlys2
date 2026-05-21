using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct FrictionMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct FrictionMovementPreContext
{
    public FrictionMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct FrictionMovementPostContext
{
    public FrictionMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnFrictionMovementPreDelegate( ref FrictionMovementPreContext ctx );
public delegate void OnFrictionMovementPostDelegate( ref FrictionMovementPostContext ctx );

public interface IFrictionMovementHook
{
    public event OnFrictionMovementPreDelegate Pre;
    public event OnFrictionMovementPostDelegate Post;
}
