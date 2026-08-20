using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingRotateMovePreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingRotateMovePostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingRotateMovePreDelegate(ref CFuncRotatingRotateMovePreContext ctx);
public delegate void OnCFuncRotatingRotateMovePostDelegate(ref CFuncRotatingRotateMovePostContext ctx);

public interface ICFuncRotatingRotateMoveHook
{
    public event OnCFuncRotatingRotateMovePreDelegate Pre;
    public event OnCFuncRotatingRotateMovePostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}