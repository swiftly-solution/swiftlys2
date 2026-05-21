using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct CheckJumpButtonLegacyMovementParams
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
}

public ref struct CheckJumpButtonLegacyMovementPreContext
{
    public CheckJumpButtonLegacyMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CheckJumpButtonLegacyMovementPostContext
{
    public CheckJumpButtonLegacyMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCheckJumpButtonLegacyMovementPreDelegate( ref CheckJumpButtonLegacyMovementPreContext ctx );
public delegate void OnCheckJumpButtonLegacyMovementPostDelegate( ref CheckJumpButtonLegacyMovementPostContext ctx );

public interface ICheckJumpButtonLegacyMovementHook
{
    public event OnCheckJumpButtonLegacyMovementPreDelegate Pre;
    public event OnCheckJumpButtonLegacyMovementPostDelegate Post;
}
