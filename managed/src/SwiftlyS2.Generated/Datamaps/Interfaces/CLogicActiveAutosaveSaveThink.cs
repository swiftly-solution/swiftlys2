using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CLogicActiveAutosaveSaveThinkPreContext
{
    public CLogicActiveAutosave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CLogicActiveAutosaveSaveThinkPostContext
{
    public CLogicActiveAutosave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCLogicActiveAutosaveSaveThinkPreDelegate(ref CLogicActiveAutosaveSaveThinkPreContext ctx);
public delegate void OnCLogicActiveAutosaveSaveThinkPostDelegate(ref CLogicActiveAutosaveSaveThinkPostContext ctx);

public interface ICLogicActiveAutosaveSaveThinkHook
{
    public event OnCLogicActiveAutosaveSaveThinkPreDelegate Pre;
    public event OnCLogicActiveAutosaveSaveThinkPostDelegate Post;

    public void Invoke(CLogicActiveAutosave schemaObject);
}