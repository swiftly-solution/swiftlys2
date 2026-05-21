using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CheckJumpButtonModernMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CheckJumpButtonModernMovementPreContext
{
    public CheckJumpButtonModernMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CheckJumpButtonModernMovementPostContext
{
    public CheckJumpButtonModernMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCheckJumpButtonModernMovementPreDelegate( ref CheckJumpButtonModernMovementPreContext ctx );
public delegate void OnCheckJumpButtonModernMovementPostDelegate( ref CheckJumpButtonModernMovementPostContext ctx );

public interface ICheckJumpButtonModernMovementHook
{
    public event OnCheckJumpButtonModernMovementPreDelegate Pre;
    public event OnCheckJumpButtonModernMovementPostDelegate Post;
}
