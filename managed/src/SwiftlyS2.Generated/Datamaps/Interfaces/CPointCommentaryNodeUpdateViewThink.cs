using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointCommentaryNodeUpdateViewThinkPreContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointCommentaryNodeUpdateViewThinkPostContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointCommentaryNodeUpdateViewThinkPreDelegate(ref CPointCommentaryNodeUpdateViewThinkPreContext ctx);
public delegate void OnCPointCommentaryNodeUpdateViewThinkPostDelegate(ref CPointCommentaryNodeUpdateViewThinkPostContext ctx);

public interface ICPointCommentaryNodeUpdateViewThinkHook
{
    public event OnCPointCommentaryNodeUpdateViewThinkPreDelegate Pre;
    public event OnCPointCommentaryNodeUpdateViewThinkPostDelegate Post;

    public void Invoke(CPointCommentaryNode schemaObject);
}