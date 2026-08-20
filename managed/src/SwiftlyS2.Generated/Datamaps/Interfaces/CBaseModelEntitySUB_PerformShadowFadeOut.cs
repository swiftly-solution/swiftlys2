using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_PerformShadowFadeOutPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_PerformShadowFadeOutPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_PerformShadowFadeOutPreDelegate(ref CBaseModelEntitySUB_PerformShadowFadeOutPreContext ctx);
public delegate void OnCBaseModelEntitySUB_PerformShadowFadeOutPostDelegate(ref CBaseModelEntitySUB_PerformShadowFadeOutPostContext ctx);

public interface ICBaseModelEntitySUB_PerformShadowFadeOutHook
{
    public event OnCBaseModelEntitySUB_PerformShadowFadeOutPreDelegate Pre;
    public event OnCBaseModelEntitySUB_PerformShadowFadeOutPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}