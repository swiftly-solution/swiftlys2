using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeSmokePreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeSmokePostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeSmokePreDelegate(ref CBaseGrenadeSmokePreContext ctx);
public delegate void OnCBaseGrenadeSmokePostDelegate(ref CBaseGrenadeSmokePostContext ctx);

public interface ICBaseGrenadeSmokeHook
{
    public event OnCBaseGrenadeSmokePreDelegate Pre;
    public event OnCBaseGrenadeSmokePostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}