using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingSpinUpMovePreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingSpinUpMovePostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingSpinUpMovePreDelegate(ref CFuncRotatingSpinUpMovePreContext ctx);
public delegate void OnCFuncRotatingSpinUpMovePostDelegate(ref CFuncRotatingSpinUpMovePostContext ctx);

public interface ICFuncRotatingSpinUpMoveHook
{
    public event OnCFuncRotatingSpinUpMovePreDelegate Pre;
    public event OnCFuncRotatingSpinUpMovePostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}