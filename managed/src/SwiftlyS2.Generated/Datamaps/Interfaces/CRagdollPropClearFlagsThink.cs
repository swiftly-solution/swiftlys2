using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRagdollPropClearFlagsThinkPreContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRagdollPropClearFlagsThinkPostContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRagdollPropClearFlagsThinkPreDelegate(ref CRagdollPropClearFlagsThinkPreContext ctx);
public delegate void OnCRagdollPropClearFlagsThinkPostDelegate(ref CRagdollPropClearFlagsThinkPostContext ctx);

public interface ICRagdollPropClearFlagsThinkHook
{
    public event OnCRagdollPropClearFlagsThinkPreDelegate Pre;
    public event OnCRagdollPropClearFlagsThinkPostDelegate Post;

    public void Invoke(CRagdollProp schemaObject);
}