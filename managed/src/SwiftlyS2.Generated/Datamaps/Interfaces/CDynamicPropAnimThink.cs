using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CDynamicPropAnimThinkPreContext
{
    public CDynamicProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CDynamicPropAnimThinkPostContext
{
    public CDynamicProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCDynamicPropAnimThinkPreDelegate(ref CDynamicPropAnimThinkPreContext ctx);
public delegate void OnCDynamicPropAnimThinkPostDelegate(ref CDynamicPropAnimThinkPostContext ctx);

public interface ICDynamicPropAnimThinkHook
{
    public event OnCDynamicPropAnimThinkPreDelegate Pre;
    public event OnCDynamicPropAnimThinkPostDelegate Post;

    public void Invoke(CDynamicProp schemaObject);
}