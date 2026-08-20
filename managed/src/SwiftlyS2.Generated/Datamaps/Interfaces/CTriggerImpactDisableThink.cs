using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerImpactDisableThinkPreContext
{
    public CTriggerImpact SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerImpactDisableThinkPostContext
{
    public CTriggerImpact SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerImpactDisableThinkPreDelegate(ref CTriggerImpactDisableThinkPreContext ctx);
public delegate void OnCTriggerImpactDisableThinkPostDelegate(ref CTriggerImpactDisableThinkPostContext ctx);

public interface ICTriggerImpactDisableThinkHook
{
    public event OnCTriggerImpactDisableThinkPreDelegate Pre;
    public event OnCTriggerImpactDisableThinkPostDelegate Post;

    public void Invoke(CTriggerImpact schemaObject);
}