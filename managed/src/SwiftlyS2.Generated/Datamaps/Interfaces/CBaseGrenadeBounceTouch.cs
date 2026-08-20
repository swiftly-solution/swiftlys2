using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeBounceTouchPreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeBounceTouchPostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeBounceTouchPreDelegate(ref CBaseGrenadeBounceTouchPreContext ctx);
public delegate void OnCBaseGrenadeBounceTouchPostDelegate(ref CBaseGrenadeBounceTouchPostContext ctx);

public interface ICBaseGrenadeBounceTouchHook
{
    public event OnCBaseGrenadeBounceTouchPreDelegate Pre;
    public event OnCBaseGrenadeBounceTouchPostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}