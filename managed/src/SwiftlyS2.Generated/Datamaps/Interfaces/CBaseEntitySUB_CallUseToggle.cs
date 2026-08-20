using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntitySUB_CallUseTogglePreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntitySUB_CallUseTogglePostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntitySUB_CallUseTogglePreDelegate(ref CBaseEntitySUB_CallUseTogglePreContext ctx);
public delegate void OnCBaseEntitySUB_CallUseTogglePostDelegate(ref CBaseEntitySUB_CallUseTogglePostContext ctx);

public interface ICBaseEntitySUB_CallUseToggleHook
{
    public event OnCBaseEntitySUB_CallUseTogglePreDelegate Pre;
    public event OnCBaseEntitySUB_CallUseTogglePostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}