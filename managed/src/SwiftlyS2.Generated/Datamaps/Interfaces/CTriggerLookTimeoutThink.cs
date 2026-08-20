using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerLookTimeoutThinkPreContext
{
    public CTriggerLook SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerLookTimeoutThinkPostContext
{
    public CTriggerLook SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerLookTimeoutThinkPreDelegate(ref CTriggerLookTimeoutThinkPreContext ctx);
public delegate void OnCTriggerLookTimeoutThinkPostDelegate(ref CTriggerLookTimeoutThinkPostContext ctx);

public interface ICTriggerLookTimeoutThinkHook
{
    public event OnCTriggerLookTimeoutThinkPreDelegate Pre;
    public event OnCTriggerLookTimeoutThinkPostDelegate Post;

    public void Invoke(CTriggerLook schemaObject);
}