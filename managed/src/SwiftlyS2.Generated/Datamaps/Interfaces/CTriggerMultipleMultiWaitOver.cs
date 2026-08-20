using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerMultipleMultiWaitOverPreContext
{
    public CTriggerMultiple SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerMultipleMultiWaitOverPostContext
{
    public CTriggerMultiple SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerMultipleMultiWaitOverPreDelegate(ref CTriggerMultipleMultiWaitOverPreContext ctx);
public delegate void OnCTriggerMultipleMultiWaitOverPostDelegate(ref CTriggerMultipleMultiWaitOverPostContext ctx);

public interface ICTriggerMultipleMultiWaitOverHook
{
    public event OnCTriggerMultipleMultiWaitOverPreDelegate Pre;
    public event OnCTriggerMultipleMultiWaitOverPostDelegate Post;

    public void Invoke(CTriggerMultiple schemaObject);
}