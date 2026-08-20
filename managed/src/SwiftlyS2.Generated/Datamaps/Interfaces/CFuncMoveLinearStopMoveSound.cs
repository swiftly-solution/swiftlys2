using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncMoveLinearStopMoveSoundPreContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncMoveLinearStopMoveSoundPostContext
{
    public CFuncMoveLinear SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncMoveLinearStopMoveSoundPreDelegate(ref CFuncMoveLinearStopMoveSoundPreContext ctx);
public delegate void OnCFuncMoveLinearStopMoveSoundPostDelegate(ref CFuncMoveLinearStopMoveSoundPostContext ctx);

public interface ICFuncMoveLinearStopMoveSoundHook
{
    public event OnCFuncMoveLinearStopMoveSoundPreDelegate Pre;
    public event OnCFuncMoveLinearStopMoveSoundPostDelegate Post;

    public void Invoke(CFuncMoveLinear schemaObject);
}