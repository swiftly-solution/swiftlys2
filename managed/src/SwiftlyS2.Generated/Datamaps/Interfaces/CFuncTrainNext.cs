using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrainNextPreContext
{
    public CFuncTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrainNextPostContext
{
    public CFuncTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrainNextPreDelegate(ref CFuncTrainNextPreContext ctx);
public delegate void OnCFuncTrainNextPostDelegate(ref CFuncTrainNextPostContext ctx);

public interface ICFuncTrainNextHook
{
    public event OnCFuncTrainNextPreDelegate Pre;
    public event OnCFuncTrainNextPostDelegate Post;

    public void Invoke(CFuncTrain schemaObject);
}