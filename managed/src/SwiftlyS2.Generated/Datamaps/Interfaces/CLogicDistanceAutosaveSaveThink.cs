using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CLogicDistanceAutosaveSaveThinkPreContext
{
    public CLogicDistanceAutosave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CLogicDistanceAutosaveSaveThinkPostContext
{
    public CLogicDistanceAutosave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCLogicDistanceAutosaveSaveThinkPreDelegate(ref CLogicDistanceAutosaveSaveThinkPreContext ctx);
public delegate void OnCLogicDistanceAutosaveSaveThinkPostDelegate(ref CLogicDistanceAutosaveSaveThinkPostContext ctx);

public interface ICLogicDistanceAutosaveSaveThinkHook
{
    public event OnCLogicDistanceAutosaveSaveThinkPreDelegate Pre;
    public event OnCLogicDistanceAutosaveSaveThinkPostDelegate Post;

    public void Invoke(CLogicDistanceAutosave schemaObject);
}