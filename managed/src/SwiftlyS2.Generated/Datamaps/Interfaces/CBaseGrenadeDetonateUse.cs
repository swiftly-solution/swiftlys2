using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeDetonateUsePreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeDetonateUsePostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeDetonateUsePreDelegate(ref CBaseGrenadeDetonateUsePreContext ctx);
public delegate void OnCBaseGrenadeDetonateUsePostDelegate(ref CBaseGrenadeDetonateUsePostContext ctx);

public interface ICBaseGrenadeDetonateUseHook
{
    public event OnCBaseGrenadeDetonateUsePreDelegate Pre;
    public event OnCBaseGrenadeDetonateUsePostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}