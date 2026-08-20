using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeDetonatePreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeDetonatePostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeDetonatePreDelegate(ref CBaseGrenadeDetonatePreContext ctx);
public delegate void OnCBaseGrenadeDetonatePostDelegate(ref CBaseGrenadeDetonatePostContext ctx);

public interface ICBaseGrenadeDetonateHook
{
    public event OnCBaseGrenadeDetonatePreDelegate Pre;
    public event OnCBaseGrenadeDetonatePostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}