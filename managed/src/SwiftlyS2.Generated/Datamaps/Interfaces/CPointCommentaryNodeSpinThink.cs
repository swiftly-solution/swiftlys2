using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPointCommentaryNodeSpinThinkPreContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPointCommentaryNodeSpinThinkPostContext
{
    public CPointCommentaryNode SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPointCommentaryNodeSpinThinkPreDelegate(ref CPointCommentaryNodeSpinThinkPreContext ctx);
public delegate void OnCPointCommentaryNodeSpinThinkPostDelegate(ref CPointCommentaryNodeSpinThinkPostContext ctx);

public interface ICPointCommentaryNodeSpinThinkHook
{
    public event OnCPointCommentaryNodeSpinThinkPreDelegate Pre;
    public event OnCPointCommentaryNodeSpinThinkPostDelegate Post;

    public void Invoke(CPointCommentaryNode schemaObject);
}