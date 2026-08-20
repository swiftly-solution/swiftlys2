using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerLerpObjectUnsetWaitForEntityPreContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerLerpObjectUnsetWaitForEntityPostContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerLerpObjectUnsetWaitForEntityPreDelegate(ref CTriggerLerpObjectUnsetWaitForEntityPreContext ctx);
public delegate void OnCTriggerLerpObjectUnsetWaitForEntityPostDelegate(ref CTriggerLerpObjectUnsetWaitForEntityPostContext ctx);

public interface ICTriggerLerpObjectUnsetWaitForEntityHook
{
    public event OnCTriggerLerpObjectUnsetWaitForEntityPreDelegate Pre;
    public event OnCTriggerLerpObjectUnsetWaitForEntityPostDelegate Post;

    public void Invoke(CTriggerLerpObject schemaObject);
}