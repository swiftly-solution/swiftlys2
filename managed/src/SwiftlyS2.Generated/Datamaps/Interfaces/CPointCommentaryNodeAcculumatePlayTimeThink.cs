using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointCommentaryNodeAcculumatePlayTimeThinkPreContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointCommentaryNodeAcculumatePlayTimeThinkPostContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointCommentaryNodeAcculumatePlayTimeThinkPreDelegate(ref CPointCommentaryNodeAcculumatePlayTimeThinkPreContext ctx);
public delegate void OnCPointCommentaryNodeAcculumatePlayTimeThinkPostDelegate(ref CPointCommentaryNodeAcculumatePlayTimeThinkPostContext ctx);

public interface ICPointCommentaryNodeAcculumatePlayTimeThinkHook
{
    public event OnCPointCommentaryNodeAcculumatePlayTimeThinkPreDelegate Pre;
    public event OnCPointCommentaryNodeAcculumatePlayTimeThinkPostDelegate Post;

    public void Invoke(CPointCommentaryNode schemaObject);
}