using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CBaseButtonTriggerAndWaitPreContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CBaseButtonTriggerAndWaitPostContext
{
    public CBaseButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCBaseButtonTriggerAndWaitPreDelegate(ref CBaseButtonTriggerAndWaitPreContext ctx);
public delegate void OnCBaseButtonTriggerAndWaitPostDelegate(ref CBaseButtonTriggerAndWaitPostContext ctx);

public interface ICBaseButtonTriggerAndWaitHook
{
    public event OnCBaseButtonTriggerAndWaitPreDelegate Pre;
    public event OnCBaseButtonTriggerAndWaitPostDelegate Post;

    public void Invoke(CBaseButton schemaObject);
}