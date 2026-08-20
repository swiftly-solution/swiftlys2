using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerLerpObjectAttachedEntityThinkPreContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerLerpObjectAttachedEntityThinkPostContext
{
    public CTriggerLerpObject SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerLerpObjectAttachedEntityThinkPreDelegate(ref CTriggerLerpObjectAttachedEntityThinkPreContext ctx);
public delegate void OnCTriggerLerpObjectAttachedEntityThinkPostDelegate(ref CTriggerLerpObjectAttachedEntityThinkPostContext ctx);

public interface ICTriggerLerpObjectAttachedEntityThinkHook
{
    public event OnCTriggerLerpObjectAttachedEntityThinkPreDelegate Pre;
    public event OnCTriggerLerpObjectAttachedEntityThinkPostDelegate Post;

    public void Invoke(CTriggerLerpObject schemaObject);
}