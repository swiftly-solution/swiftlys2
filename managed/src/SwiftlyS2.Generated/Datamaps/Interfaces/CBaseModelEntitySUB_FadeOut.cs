using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseModelEntitySUB_FadeOutPreContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseModelEntitySUB_FadeOutPostContext
{
    public CBaseModelEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseModelEntitySUB_FadeOutPreDelegate(ref CBaseModelEntitySUB_FadeOutPreContext ctx);
public delegate void OnCBaseModelEntitySUB_FadeOutPostDelegate(ref CBaseModelEntitySUB_FadeOutPostContext ctx);

public interface ICBaseModelEntitySUB_FadeOutHook
{
    public event OnCBaseModelEntitySUB_FadeOutPreDelegate Pre;
    public event OnCBaseModelEntitySUB_FadeOutPostDelegate Post;

    public void Invoke(CBaseModelEntity schemaObject);
}