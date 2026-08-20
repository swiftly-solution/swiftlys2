using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncMoveLinearNavMovableThinkPreContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncMoveLinearNavMovableThinkPostContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncMoveLinearNavMovableThinkPreDelegate(ref CFuncMoveLinearNavMovableThinkPreContext ctx);
public delegate void OnCFuncMoveLinearNavMovableThinkPostDelegate(ref CFuncMoveLinearNavMovableThinkPostContext ctx);

public interface ICFuncMoveLinearNavMovableThinkHook
{
    public event OnCFuncMoveLinearNavMovableThinkPreDelegate Pre;
    public event OnCFuncMoveLinearNavMovableThinkPostDelegate Post;

    public void Invoke(CFuncMoveLinear schemaObject);
}