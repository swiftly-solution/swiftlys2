using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingRotatingUsePreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingRotatingUsePostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingRotatingUsePreDelegate(ref CFuncRotatingRotatingUsePreContext ctx);
public delegate void OnCFuncRotatingRotatingUsePostDelegate(ref CFuncRotatingRotatingUsePostContext ctx);

public interface ICFuncRotatingRotatingUseHook
{
    public event OnCFuncRotatingRotatingUsePreDelegate Pre;
    public event OnCFuncRotatingRotatingUsePostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}