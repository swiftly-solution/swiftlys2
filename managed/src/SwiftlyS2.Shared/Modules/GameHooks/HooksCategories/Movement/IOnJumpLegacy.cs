using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct OnJumpLegacyMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct OnJumpLegacyMovementPreContext
{
    public OnJumpLegacyMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct OnJumpLegacyMovementPostContext
{
    public OnJumpLegacyMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnOnJumpLegacyMovementPreDelegate( ref OnJumpLegacyMovementPreContext ctx );
public delegate void OnOnJumpLegacyMovementPostDelegate( ref OnJumpLegacyMovementPostContext ctx );

public interface IOnJumpLegacyMovementHook
{
    public event OnOnJumpLegacyMovementPreDelegate Pre;
    public event OnOnJumpLegacyMovementPostDelegate Post;
}
