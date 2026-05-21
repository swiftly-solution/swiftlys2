using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct ProcessUsercmdsParams
{
    public required IPlayer Player { get; set; }
    public required List<IUserCmd> Usercmds { get; init; }
    public required bool Paused { get; init; }
    public required float Margin { get; init; }
}

public ref struct ProcessUsercmdsPreContext
{
    public ProcessUsercmdsParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct ProcessUsercmdsPostContext
{
    public ProcessUsercmdsParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnProcessUsercmdsPreDelegate( ref ProcessUsercmdsPreContext ctx );
public delegate void OnProcessUsercmdsPostDelegate( ref ProcessUsercmdsPostContext ctx );

public interface IProcessUsercmdsHook
{
    public event OnProcessUsercmdsPreDelegate Pre;
    public event OnProcessUsercmdsPostDelegate Post;
}
