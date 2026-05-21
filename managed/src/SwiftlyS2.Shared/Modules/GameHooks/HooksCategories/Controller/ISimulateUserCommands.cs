using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct SimulateUserCommandsParams
{
    public required IPlayer Player { get; set; }
}

public ref struct SimulateUserCommandsPreContext
{
    public SimulateUserCommandsParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct SimulateUserCommandsPostContext
{
    public SimulateUserCommandsParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnSimulateUserCommandsPreDelegate( ref SimulateUserCommandsPreContext ctx );
public delegate void OnSimulateUserCommandsPostDelegate( ref SimulateUserCommandsPostContext ctx );

public interface ISimulateUserCommandsHook
{
    public event OnSimulateUserCommandsPreDelegate Pre;
    public event OnSimulateUserCommandsPostDelegate Post;
}
