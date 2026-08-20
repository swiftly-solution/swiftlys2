using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrackTrainFindPreContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrackTrainFindPostContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrackTrainFindPreDelegate(ref CFuncTrackTrainFindPreContext ctx);
public delegate void OnCFuncTrackTrainFindPostDelegate(ref CFuncTrackTrainFindPostContext ctx);

public interface ICFuncTrackTrainFindHook
{
    public event OnCFuncTrackTrainFindPreDelegate Pre;
    public event OnCFuncTrackTrainFindPostDelegate Post;

    public void Invoke(CFuncTrackTrain schemaObject);
}