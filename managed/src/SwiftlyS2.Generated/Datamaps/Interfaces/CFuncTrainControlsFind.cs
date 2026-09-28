using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrainControlsFindPreContext
{
    public CFuncTrainControls SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrainControlsFindPostContext
{
    public CFuncTrainControls SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrainControlsFindPreDelegate(ref CFuncTrainControlsFindPreContext ctx);
public delegate void OnCFuncTrainControlsFindPostDelegate(ref CFuncTrainControlsFindPostContext ctx);

public interface ICFuncTrainControlsFindHook
{
    public event OnCFuncTrainControlsFindPreDelegate Pre;
    public event OnCFuncTrainControlsFindPostDelegate Post;

    public void Invoke(CFuncTrainControls schemaObject);
}