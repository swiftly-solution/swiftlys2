using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrackTrainDeadEndPreContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrackTrainDeadEndPostContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrackTrainDeadEndPreDelegate(ref CFuncTrackTrainDeadEndPreContext ctx);
public delegate void OnCFuncTrackTrainDeadEndPostDelegate(ref CFuncTrackTrainDeadEndPostContext ctx);

public interface ICFuncTrackTrainDeadEndHook
{
    public event OnCFuncTrackTrainDeadEndPreDelegate Pre;
    public event OnCFuncTrackTrainDeadEndPostDelegate Post;

    public void Invoke(CFuncTrackTrain schemaObject);
}