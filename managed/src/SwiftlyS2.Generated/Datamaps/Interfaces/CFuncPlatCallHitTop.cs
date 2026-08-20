using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncPlatCallHitTopPreContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncPlatCallHitTopPostContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncPlatCallHitTopPreDelegate(ref CFuncPlatCallHitTopPreContext ctx);
public delegate void OnCFuncPlatCallHitTopPostDelegate(ref CFuncPlatCallHitTopPostContext ctx);

public interface ICFuncPlatCallHitTopHook
{
    public event OnCFuncPlatCallHitTopPreDelegate Pre;
    public event OnCFuncPlatCallHitTopPostDelegate Post;

    public void Invoke(CFuncPlat schemaObject);
}