using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRagdollPropAttachedItemsThinkPreContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRagdollPropAttachedItemsThinkPostContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRagdollPropAttachedItemsThinkPreDelegate(ref CRagdollPropAttachedItemsThinkPreContext ctx);
public delegate void OnCRagdollPropAttachedItemsThinkPostDelegate(ref CRagdollPropAttachedItemsThinkPostContext ctx);

public interface ICRagdollPropAttachedItemsThinkHook
{
    public event OnCRagdollPropAttachedItemsThinkPreDelegate Pre;
    public event OnCRagdollPropAttachedItemsThinkPostDelegate Post;

    public void Invoke(CRagdollProp schemaObject);
}