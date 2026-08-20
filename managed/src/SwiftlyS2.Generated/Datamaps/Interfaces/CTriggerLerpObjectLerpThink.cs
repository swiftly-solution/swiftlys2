using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerLerpObjectLerpThinkPreContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerLerpObjectLerpThinkPostContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerLerpObjectLerpThinkPreDelegate(ref CTriggerLerpObjectLerpThinkPreContext ctx);
public delegate void OnCTriggerLerpObjectLerpThinkPostDelegate(ref CTriggerLerpObjectLerpThinkPostContext ctx);

public interface ICTriggerLerpObjectLerpThinkHook
{
    public event OnCTriggerLerpObjectLerpThinkPreDelegate Pre;
    public event OnCTriggerLerpObjectLerpThinkPostDelegate Post;

    public void Invoke(CTriggerLerpObject schemaObject);
}