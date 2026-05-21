using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CanMovePawnParams
{
    public required IPlayer Player { get; set; }
}

public ref struct CanMovePawnPreContext
{
    public CanMovePawnParams Params;
    private bool _return;
    private bool _returnSet;
    private HookResult _hookResult;

    public void SetReturn( bool result ) { _return = result; _returnSet = true; }
    public void SetHookResult( HookResult result ) => _hookResult = result;

    internal bool IsReturnSet => _returnSet;
    internal bool Return => _return;
    internal HookResult HookResult => _hookResult;
}

public ref struct CanMovePawnPostContext
{
    public CanMovePawnParams Params;
    public bool Return { get; set; }
    private HookResult _hookResult;

    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCanMovePawnPreDelegate( ref CanMovePawnPreContext ctx );
public delegate void OnCanMovePawnPostDelegate( ref CanMovePawnPostContext ctx );

public interface ICanMovePawnHook
{
    public event OnCanMovePawnPreDelegate Pre;
    public event OnCanMovePawnPostDelegate Post;
}
