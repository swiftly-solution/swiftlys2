using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_StartShadowFadeInPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_StartShadowFadeInPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_StartShadowFadeInPreDelegate(ref CBaseModelEntitySUB_StartShadowFadeInPreContext ctx);
public delegate void OnCBaseModelEntitySUB_StartShadowFadeInPostDelegate(ref CBaseModelEntitySUB_StartShadowFadeInPostContext ctx);

public interface ICBaseModelEntitySUB_StartShadowFadeInHook
{
    public event OnCBaseModelEntitySUB_StartShadowFadeInPreDelegate Pre;
    public event OnCBaseModelEntitySUB_StartShadowFadeInPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}