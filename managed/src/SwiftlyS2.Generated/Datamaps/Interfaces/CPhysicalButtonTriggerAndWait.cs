using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonTriggerAndWaitPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonTriggerAndWaitPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonTriggerAndWaitPreDelegate(ref CPhysicalButtonTriggerAndWaitPreContext ctx);
public delegate void OnCPhysicalButtonTriggerAndWaitPostDelegate(ref CPhysicalButtonTriggerAndWaitPostContext ctx);

public interface ICPhysicalButtonTriggerAndWaitHook
{
    public event OnCPhysicalButtonTriggerAndWaitPreDelegate Pre;
    public event OnCPhysicalButtonTriggerAndWaitPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}