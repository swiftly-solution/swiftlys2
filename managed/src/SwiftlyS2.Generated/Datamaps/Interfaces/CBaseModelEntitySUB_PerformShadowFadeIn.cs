using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_PerformShadowFadeInPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_PerformShadowFadeInPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_PerformShadowFadeInPreDelegate(ref CBaseModelEntitySUB_PerformShadowFadeInPreContext ctx);
public delegate void OnCBaseModelEntitySUB_PerformShadowFadeInPostDelegate(ref CBaseModelEntitySUB_PerformShadowFadeInPostContext ctx);

public interface ICBaseModelEntitySUB_PerformShadowFadeInHook
{
    public event OnCBaseModelEntitySUB_PerformShadowFadeInPreDelegate Pre;
    public event OnCBaseModelEntitySUB_PerformShadowFadeInPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}