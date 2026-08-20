using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CRagdollPropSettleThinkPreContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CRagdollPropSettleThinkPostContext
{
    public CRagdollProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCRagdollPropSettleThinkPreDelegate(ref CRagdollPropSettleThinkPreContext ctx);
public delegate void OnCRagdollPropSettleThinkPostDelegate(ref CRagdollPropSettleThinkPostContext ctx);

public interface ICRagdollPropSettleThinkHook
{
    public event OnCRagdollPropSettleThinkPreDelegate Pre;
    public event OnCRagdollPropSettleThinkPostDelegate Post;

    public void Invoke(CRagdollProp schemaObject);
}