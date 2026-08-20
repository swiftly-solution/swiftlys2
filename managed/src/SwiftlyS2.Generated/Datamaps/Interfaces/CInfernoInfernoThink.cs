using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CInfernoInfernoThinkPreContext
{
    public CInferno SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CInfernoInfernoThinkPostContext
{
    public CInferno SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCInfernoInfernoThinkPreDelegate(ref CInfernoInfernoThinkPreContext ctx);
public delegate void OnCInfernoInfernoThinkPostDelegate(ref CInfernoInfernoThinkPostContext ctx);

public interface ICInfernoInfernoThinkHook
{
    public event OnCInfernoInfernoThinkPreDelegate Pre;
    public event OnCInfernoInfernoThinkPostDelegate Post;

    public void Invoke(CInferno schemaObject);
}