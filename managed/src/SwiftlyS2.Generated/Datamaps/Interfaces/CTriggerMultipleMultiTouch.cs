using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerMultipleMultiTouchPreContext
{
    public CTriggerMultiple SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerMultipleMultiTouchPostContext
{
    public CTriggerMultiple SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerMultipleMultiTouchPreDelegate(ref CTriggerMultipleMultiTouchPreContext ctx);
public delegate void OnCTriggerMultipleMultiTouchPostDelegate(ref CTriggerMultipleMultiTouchPostContext ctx);

public interface ICTriggerMultipleMultiTouchHook
{
    public event OnCTriggerMultipleMultiTouchPreDelegate Pre;
    public event OnCTriggerMultipleMultiTouchPostDelegate Post;

    public void Invoke(CTriggerMultiple schemaObject);
}