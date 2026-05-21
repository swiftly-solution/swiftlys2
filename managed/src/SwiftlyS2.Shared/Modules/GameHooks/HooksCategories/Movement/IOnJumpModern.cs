using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct OnJumpModernMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct OnJumpModernMovementPreContext
{
    public OnJumpModernMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct OnJumpModernMovementPostContext
{
    public OnJumpModernMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnOnJumpModernMovementPreDelegate( ref OnJumpModernMovementPreContext ctx );
public delegate void OnOnJumpModernMovementPostDelegate( ref OnJumpModernMovementPostContext ctx );

public interface IOnJumpModernMovementHook
{
    public event OnOnJumpModernMovementPreDelegate Pre;
    public event OnOnJumpModernMovementPostDelegate Post;
}
