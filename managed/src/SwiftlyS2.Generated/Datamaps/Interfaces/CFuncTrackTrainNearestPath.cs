using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncTrackTrainNearestPathPreContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncTrackTrainNearestPathPostContext
{
    public CFuncTrackTrain SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncTrackTrainNearestPathPreDelegate(ref CFuncTrackTrainNearestPathPreContext ctx);
public delegate void OnCFuncTrackTrainNearestPathPostDelegate(ref CFuncTrackTrainNearestPathPostContext ctx);

public interface ICFuncTrackTrainNearestPathHook
{
    public event OnCFuncTrackTrainNearestPathPreDelegate Pre;
    public event OnCFuncTrackTrainNearestPathPostDelegate Post;

    public void Invoke(CFuncTrackTrain schemaObject);
}