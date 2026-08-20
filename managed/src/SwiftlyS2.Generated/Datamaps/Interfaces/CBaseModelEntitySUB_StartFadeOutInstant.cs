using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_StartFadeOutInstantPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_StartFadeOutInstantPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_StartFadeOutInstantPreDelegate(ref CBaseModelEntitySUB_StartFadeOutInstantPreContext ctx);
public delegate void OnCBaseModelEntitySUB_StartFadeOutInstantPostDelegate(ref CBaseModelEntitySUB_StartFadeOutInstantPostContext ctx);

public interface ICBaseModelEntitySUB_StartFadeOutInstantHook
{
    public event OnCBaseModelEntitySUB_StartFadeOutInstantPreDelegate Pre;
    public event OnCBaseModelEntitySUB_StartFadeOutInstantPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}