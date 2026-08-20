using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMultiLightProxyApproachBrightnessThinkPreContext
{
    public CMultiLightProxy SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMultiLightProxyApproachBrightnessThinkPostContext
{
    public CMultiLightProxy SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMultiLightProxyApproachBrightnessThinkPreDelegate(ref CMultiLightProxyApproachBrightnessThinkPreContext ctx);
public delegate void OnCMultiLightProxyApproachBrightnessThinkPostDelegate(ref CMultiLightProxyApproachBrightnessThinkPostContext ctx);

public interface ICMultiLightProxyApproachBrightnessThinkHook
{
    public event OnCMultiLightProxyApproachBrightnessThinkPreDelegate Pre;
    public event OnCMultiLightProxyApproachBrightnessThinkPostDelegate Post;

    public void Invoke(CMultiLightProxy schemaObject);
}