using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingReverseMovePreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingReverseMovePostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingReverseMovePreDelegate(ref CFuncRotatingReverseMovePreContext ctx);
public delegate void OnCFuncRotatingReverseMovePostDelegate(ref CFuncRotatingReverseMovePostContext ctx);

public interface ICFuncRotatingReverseMoveHook
{
    public event OnCFuncRotatingReverseMovePreDelegate Pre;
    public event OnCFuncRotatingReverseMovePostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}