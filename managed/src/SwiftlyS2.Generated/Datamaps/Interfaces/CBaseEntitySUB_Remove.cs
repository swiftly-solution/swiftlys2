using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntitySUB_RemovePreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntitySUB_RemovePostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntitySUB_RemovePreDelegate(ref CBaseEntitySUB_RemovePreContext ctx);
public delegate void OnCBaseEntitySUB_RemovePostDelegate(ref CBaseEntitySUB_RemovePostContext ctx);

public interface ICBaseEntitySUB_RemoveHook
{
    public event OnCBaseEntitySUB_RemovePreDelegate Pre;
    public event OnCBaseEntitySUB_RemovePostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}