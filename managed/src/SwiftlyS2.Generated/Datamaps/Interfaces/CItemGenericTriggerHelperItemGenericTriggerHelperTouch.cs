using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CItemGenericTriggerHelperItemGenericTriggerHelperTouchPreContext
{
    public CItemGenericTriggerHelper SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CItemGenericTriggerHelperItemGenericTriggerHelperTouchPostContext
{
    public CItemGenericTriggerHelper SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPreDelegate(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPreContext ctx);
public delegate void OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPostDelegate(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPostContext ctx);

public interface ICItemGenericTriggerHelperItemGenericTriggerHelperTouchHook
{
    public event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPreDelegate Pre;
    public event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPostDelegate Post;

    public void Invoke(CItemGenericTriggerHelper schemaObject);
}