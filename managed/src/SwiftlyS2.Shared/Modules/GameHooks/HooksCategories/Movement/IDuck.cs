using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct DuckMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct DuckMovementPreContext
{
    public DuckMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct DuckMovementPostContext
{
    public DuckMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnDuckMovementPreDelegate( ref DuckMovementPreContext ctx );
public delegate void OnDuckMovementPostDelegate( ref DuckMovementPostContext ctx );

public interface IDuckMovementHook
{
    public event OnDuckMovementPreDelegate Pre;
    public event OnDuckMovementPostDelegate Post;
}
