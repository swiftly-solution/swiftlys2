using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_StartShadowFadeOutPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_StartShadowFadeOutPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_StartShadowFadeOutPreDelegate(ref CBaseModelEntitySUB_StartShadowFadeOutPreContext ctx);
public delegate void OnCBaseModelEntitySUB_StartShadowFadeOutPostDelegate(ref CBaseModelEntitySUB_StartShadowFadeOutPostContext ctx);

public interface ICBaseModelEntitySUB_StartShadowFadeOutHook
{
    public event OnCBaseModelEntitySUB_StartShadowFadeOutPreDelegate Pre;
    public event OnCBaseModelEntitySUB_StartShadowFadeOutPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}