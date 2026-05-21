using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CanUnduckMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CanUnduckMovementPreContext
{
    public CanUnduckMovementParams Params;
    private bool _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( bool result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal bool Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct CanUnduckMovementPostContext
{
    public CanUnduckMovementParams Params;
    public bool Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCanUnduckMovementPreDelegate( ref CanUnduckMovementPreContext ctx );
public delegate void OnCanUnduckMovementPostDelegate( ref CanUnduckMovementPostContext ctx );

public interface ICanUnduckMovementHook
{
    public event OnCanUnduckMovementPreDelegate Pre;
    public event OnCanUnduckMovementPostDelegate Post;
}
