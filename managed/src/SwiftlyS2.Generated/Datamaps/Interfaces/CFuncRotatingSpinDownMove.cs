using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingSpinDownMovePreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingSpinDownMovePostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingSpinDownMovePreDelegate(ref CFuncRotatingSpinDownMovePreContext ctx);
public delegate void OnCFuncRotatingSpinDownMovePostDelegate(ref CFuncRotatingSpinDownMovePostContext ctx);

public interface ICFuncRotatingSpinDownMoveHook
{
    public event OnCFuncRotatingSpinDownMovePreDelegate Pre;
    public event OnCFuncRotatingSpinDownMovePostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}