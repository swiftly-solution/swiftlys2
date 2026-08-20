using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncMoveLinearNavObstacleThinkPreContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncMoveLinearNavObstacleThinkPostContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncMoveLinearNavObstacleThinkPreDelegate(ref CFuncMoveLinearNavObstacleThinkPreContext ctx);
public delegate void OnCFuncMoveLinearNavObstacleThinkPostDelegate(ref CFuncMoveLinearNavObstacleThinkPostContext ctx);

public interface ICFuncMoveLinearNavObstacleThinkHook
{
    public event OnCFuncMoveLinearNavObstacleThinkPreDelegate Pre;
    public event OnCFuncMoveLinearNavObstacleThinkPostDelegate Post;

    public void Invoke(CFuncMoveLinear schemaObject);
}