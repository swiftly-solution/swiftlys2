using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseGrenadeSlideTouchPreContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseGrenadeSlideTouchPostContext
{
    public CBaseGrenade SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseGrenadeSlideTouchPreDelegate(ref CBaseGrenadeSlideTouchPreContext ctx);
public delegate void OnCBaseGrenadeSlideTouchPostDelegate(ref CBaseGrenadeSlideTouchPostContext ctx);

public interface ICBaseGrenadeSlideTouchHook
{
    public event OnCBaseGrenadeSlideTouchPreDelegate Pre;
    public event OnCBaseGrenadeSlideTouchPostDelegate Post;

    public void Invoke(CBaseGrenade schemaObject);
}