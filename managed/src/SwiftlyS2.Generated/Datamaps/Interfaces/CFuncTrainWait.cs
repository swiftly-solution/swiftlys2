using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrainWaitPreContext
{
    public CFuncTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrainWaitPostContext
{
    public CFuncTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrainWaitPreDelegate(ref CFuncTrainWaitPreContext ctx);
public delegate void OnCFuncTrainWaitPostDelegate(ref CFuncTrainWaitPostContext ctx);

public interface ICFuncTrainWaitHook
{
    public event OnCFuncTrainWaitPreDelegate Pre;
    public event OnCFuncTrainWaitPostDelegate Post;

    public void Invoke(CFuncTrain schemaObject);
}