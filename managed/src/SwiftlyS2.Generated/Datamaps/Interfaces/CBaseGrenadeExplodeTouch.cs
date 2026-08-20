using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeExplodeTouchPreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeExplodeTouchPostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeExplodeTouchPreDelegate(ref CBaseGrenadeExplodeTouchPreContext ctx);
public delegate void OnCBaseGrenadeExplodeTouchPostDelegate(ref CBaseGrenadeExplodeTouchPostContext ctx);

public interface ICBaseGrenadeExplodeTouchHook
{
    public event OnCBaseGrenadeExplodeTouchPreDelegate Pre;
    public event OnCBaseGrenadeExplodeTouchPostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}