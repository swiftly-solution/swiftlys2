using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerProximityMeasureThinkPreContext
{
    public CTriggerProximity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerProximityMeasureThinkPostContext
{
    public CTriggerProximity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerProximityMeasureThinkPreDelegate(ref CTriggerProximityMeasureThinkPreContext ctx);
public delegate void OnCTriggerProximityMeasureThinkPostDelegate(ref CTriggerProximityMeasureThinkPostContext ctx);

public interface ICTriggerProximityMeasureThinkHook
{
    public event OnCTriggerProximityMeasureThinkPreDelegate Pre;
    public event OnCTriggerProximityMeasureThinkPostDelegate Post;

    public void Invoke(CTriggerProximity schemaObject);
}