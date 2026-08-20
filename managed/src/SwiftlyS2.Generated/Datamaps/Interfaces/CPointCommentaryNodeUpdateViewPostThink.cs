using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointCommentaryNodeUpdateViewPostThinkPreContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointCommentaryNodeUpdateViewPostThinkPostContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointCommentaryNodeUpdateViewPostThinkPreDelegate(ref CPointCommentaryNodeUpdateViewPostThinkPreContext ctx);
public delegate void OnCPointCommentaryNodeUpdateViewPostThinkPostDelegate(ref CPointCommentaryNodeUpdateViewPostThinkPostContext ctx);

public interface ICPointCommentaryNodeUpdateViewPostThinkHook
{
    public event OnCPointCommentaryNodeUpdateViewPostThinkPreDelegate Pre;
    public event OnCPointCommentaryNodeUpdateViewPostThinkPostDelegate Post;

    public void Invoke(CPointCommentaryNode schemaObject);
}