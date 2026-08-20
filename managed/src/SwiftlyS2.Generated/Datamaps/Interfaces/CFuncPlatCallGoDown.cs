using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncPlatCallGoDownPreContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncPlatCallGoDownPostContext
{
    public CFuncPlat SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncPlatCallGoDownPreDelegate(ref CFuncPlatCallGoDownPreContext ctx);
public delegate void OnCFuncPlatCallGoDownPostDelegate(ref CFuncPlatCallGoDownPostContext ctx);

public interface ICFuncPlatCallGoDownHook
{
    public event OnCFuncPlatCallGoDownPreDelegate Pre;
    public event OnCFuncPlatCallGoDownPostDelegate Post;

    public void Invoke(CFuncPlat schemaObject);
}