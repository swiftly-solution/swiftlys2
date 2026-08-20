using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRagdollPropFadeOutThinkPreContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRagdollPropFadeOutThinkPostContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRagdollPropFadeOutThinkPreDelegate(ref CRagdollPropFadeOutThinkPreContext ctx);
public delegate void OnCRagdollPropFadeOutThinkPostDelegate(ref CRagdollPropFadeOutThinkPostContext ctx);

public interface ICRagdollPropFadeOutThinkHook
{
    public event OnCRagdollPropFadeOutThinkPreDelegate Pre;
    public event OnCRagdollPropFadeOutThinkPostDelegate Post;

    public void Invoke(CRagdollProp schemaObject);
}