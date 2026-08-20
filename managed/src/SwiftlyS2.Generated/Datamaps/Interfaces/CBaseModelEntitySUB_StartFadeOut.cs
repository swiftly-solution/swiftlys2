using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_StartFadeOutPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_StartFadeOutPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_StartFadeOutPreDelegate(ref CBaseModelEntitySUB_StartFadeOutPreContext ctx);
public delegate void OnCBaseModelEntitySUB_StartFadeOutPostDelegate(ref CBaseModelEntitySUB_StartFadeOutPostContext ctx);

public interface ICBaseModelEntitySUB_StartFadeOutHook
{
    public event OnCBaseModelEntitySUB_StartFadeOutPreDelegate Pre;
    public event OnCBaseModelEntitySUB_StartFadeOutPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}