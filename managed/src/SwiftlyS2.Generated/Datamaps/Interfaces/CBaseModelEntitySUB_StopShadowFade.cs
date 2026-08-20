using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_StopShadowFadePreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_StopShadowFadePostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_StopShadowFadePreDelegate(ref CBaseModelEntitySUB_StopShadowFadePreContext ctx);
public delegate void OnCBaseModelEntitySUB_StopShadowFadePostDelegate(ref CBaseModelEntitySUB_StopShadowFadePostContext ctx);

public interface ICBaseModelEntitySUB_StopShadowFadeHook
{
    public event OnCBaseModelEntitySUB_StopShadowFadePreDelegate Pre;
    public event OnCBaseModelEntitySUB_StopShadowFadePostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}