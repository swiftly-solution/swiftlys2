using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerSaveRetriggerWaitOverPreContext
{
    public CTriggerSave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerSaveRetriggerWaitOverPostContext
{
    public CTriggerSave SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerSaveRetriggerWaitOverPreDelegate(ref CTriggerSaveRetriggerWaitOverPreContext ctx);
public delegate void OnCTriggerSaveRetriggerWaitOverPostDelegate(ref CTriggerSaveRetriggerWaitOverPostContext ctx);

public interface ICTriggerSaveRetriggerWaitOverHook
{
    public event OnCTriggerSaveRetriggerWaitOverPreDelegate Pre;
    public event OnCTriggerSaveRetriggerWaitOverPostDelegate Post;

    public void Invoke(CTriggerSave schemaObject);
}