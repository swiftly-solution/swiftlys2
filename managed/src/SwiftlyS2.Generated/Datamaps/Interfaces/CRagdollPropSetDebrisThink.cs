using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRagdollPropSetDebrisThinkPreContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRagdollPropSetDebrisThinkPostContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRagdollPropSetDebrisThinkPreDelegate(ref CRagdollPropSetDebrisThinkPreContext ctx);
public delegate void OnCRagdollPropSetDebrisThinkPostDelegate(ref CRagdollPropSetDebrisThinkPostContext ctx);

public interface ICRagdollPropSetDebrisThinkHook
{
    public event OnCRagdollPropSetDebrisThinkPreDelegate Pre;
    public event OnCRagdollPropSetDebrisThinkPostDelegate Post;

    public void Invoke(CRagdollProp schemaObject);
}