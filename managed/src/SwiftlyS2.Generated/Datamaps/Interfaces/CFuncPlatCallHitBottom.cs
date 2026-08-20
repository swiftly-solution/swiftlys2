using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncPlatCallHitBottomPreContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncPlatCallHitBottomPostContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncPlatCallHitBottomPreDelegate(ref CFuncPlatCallHitBottomPreContext ctx);
public delegate void OnCFuncPlatCallHitBottomPostDelegate(ref CFuncPlatCallHitBottomPostContext ctx);

public interface ICFuncPlatCallHitBottomHook
{
    public event OnCFuncPlatCallHitBottomPreDelegate Pre;
    public event OnCFuncPlatCallHitBottomPostDelegate Post;

    public void Invoke(CFuncPlat schemaObject);
}