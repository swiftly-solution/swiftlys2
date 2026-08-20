using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CMultiLightProxyRestoreFlashlightThinkPreContext
{
    public CMultiLightProxy SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CMultiLightProxyRestoreFlashlightThinkPostContext
{
    public CMultiLightProxy SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCMultiLightProxyRestoreFlashlightThinkPreDelegate(ref CMultiLightProxyRestoreFlashlightThinkPreContext ctx);
public delegate void OnCMultiLightProxyRestoreFlashlightThinkPostDelegate(ref CMultiLightProxyRestoreFlashlightThinkPostContext ctx);

public interface ICMultiLightProxyRestoreFlashlightThinkHook
{
    public event OnCMultiLightProxyRestoreFlashlightThinkPreDelegate Pre;
    public event OnCMultiLightProxyRestoreFlashlightThinkPostDelegate Post;

    public void Invoke(CMultiLightProxy schemaObject);
}