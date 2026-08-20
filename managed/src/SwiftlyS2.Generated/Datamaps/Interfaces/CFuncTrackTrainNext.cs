using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrackTrainNextPreContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrackTrainNextPostContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrackTrainNextPreDelegate(ref CFuncTrackTrainNextPreContext ctx);
public delegate void OnCFuncTrackTrainNextPostDelegate(ref CFuncTrackTrainNextPostContext ctx);

public interface ICFuncTrackTrainNextHook
{
    public event OnCFuncTrackTrainNextPreDelegate Pre;
    public event OnCFuncTrackTrainNextPostDelegate Post;

    public void Invoke(CFuncTrackTrain schemaObject);
}