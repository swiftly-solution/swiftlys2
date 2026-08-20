using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CDynamicLightDynamicLightThinkPreContext
{
    public CDynamicLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CDynamicLightDynamicLightThinkPostContext
{
    public CDynamicLight SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCDynamicLightDynamicLightThinkPreDelegate(ref CDynamicLightDynamicLightThinkPreContext ctx);
public delegate void OnCDynamicLightDynamicLightThinkPostDelegate(ref CDynamicLightDynamicLightThinkPostContext ctx);

public interface ICDynamicLightDynamicLightThinkHook
{
    public event OnCDynamicLightDynamicLightThinkPreDelegate Pre;
    public event OnCDynamicLightDynamicLightThinkPostDelegate Post;

    public void Invoke(CDynamicLight schemaObject);
}