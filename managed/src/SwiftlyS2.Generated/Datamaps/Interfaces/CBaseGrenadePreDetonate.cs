using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadePreDetonatePreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadePreDetonatePostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadePreDetonatePreDelegate(ref CBaseGrenadePreDetonatePreContext ctx);
public delegate void OnCBaseGrenadePreDetonatePostDelegate(ref CBaseGrenadePreDetonatePostContext ctx);

public interface ICBaseGrenadePreDetonateHook
{
    public event OnCBaseGrenadePreDetonatePreDelegate Pre;
    public event OnCBaseGrenadePreDetonatePostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}