using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntitySUB_KillSelfPreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntitySUB_KillSelfPostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntitySUB_KillSelfPreDelegate(ref CBaseEntitySUB_KillSelfPreContext ctx);
public delegate void OnCBaseEntitySUB_KillSelfPostDelegate(ref CBaseEntitySUB_KillSelfPostContext ctx);

public interface ICBaseEntitySUB_KillSelfHook
{
    public event OnCBaseEntitySUB_KillSelfPreDelegate Pre;
    public event OnCBaseEntitySUB_KillSelfPostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}