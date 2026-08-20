using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncPlatPlatUsePreContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncPlatPlatUsePostContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncPlatPlatUsePreDelegate(ref CFuncPlatPlatUsePreContext ctx);
public delegate void OnCFuncPlatPlatUsePostDelegate(ref CFuncPlatPlatUsePostContext ctx);

public interface ICFuncPlatPlatUseHook
{
    public event OnCFuncPlatPlatUsePreDelegate Pre;
    public event OnCFuncPlatPlatUsePostDelegate Post;

    public void Invoke(CFuncPlat schemaObject);
}