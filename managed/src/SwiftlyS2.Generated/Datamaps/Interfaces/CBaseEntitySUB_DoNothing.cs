using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseEntitySUB_DoNothingPreContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseEntitySUB_DoNothingPostContext
{
    public CBaseEntity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseEntitySUB_DoNothingPreDelegate(ref CBaseEntitySUB_DoNothingPreContext ctx);
public delegate void OnCBaseEntitySUB_DoNothingPostDelegate(ref CBaseEntitySUB_DoNothingPostContext ctx);

public interface ICBaseEntitySUB_DoNothingHook
{
    public event OnCBaseEntitySUB_DoNothingPreDelegate Pre;
    public event OnCBaseEntitySUB_DoNothingPostDelegate Post;

    public void Invoke(CBaseEntity schemaObject);
}