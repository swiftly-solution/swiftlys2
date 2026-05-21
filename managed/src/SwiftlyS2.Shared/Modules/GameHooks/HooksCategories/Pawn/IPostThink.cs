using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public struct PostThinkPawnParams
{
    public required IPlayer Player { get; set; }
}

public ref struct PostThinkPawnPreContext
{
    public PostThinkPawnParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct PostThinkPawnPostContext
{
    public PostThinkPawnParams Params;
    private HookResult _hookResult;
    public void SetHookResult( HookResult result ) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnPostThinkPawnPreDelegate( ref PostThinkPawnPreContext ctx );
public delegate void OnPostThinkPawnPostDelegate( ref PostThinkPawnPostContext ctx );

public interface IPostThinkPawnHook
{
    public event OnPostThinkPawnPreDelegate Pre;
    public event OnPostThinkPawnPostDelegate Post;
}
