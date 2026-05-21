using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct RunCommandMovementParams
{
    public required IPlayer Player { get; set; }
    public required IUserCmd UserCmd { get; init; }
}

public ref struct RunCommandMovementPreContext
{
    public RunCommandMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct RunCommandMovementPostContext
{
    public RunCommandMovementParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnRunCommandMovementPreDelegate( ref RunCommandMovementPreContext ctx );
public delegate void OnRunCommandMovementPostDelegate( ref RunCommandMovementPostContext ctx );

public interface IRunCommandMovementHook
{
    public event OnRunCommandMovementPreDelegate Pre;
    public event OnRunCommandMovementPostDelegate Post;
}
