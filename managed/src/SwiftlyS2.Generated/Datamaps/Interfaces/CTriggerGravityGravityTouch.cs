using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CTriggerGravityGravityTouchPreContext
{
    public CTriggerGravity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CTriggerGravityGravityTouchPostContext
{
    public CTriggerGravity SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCTriggerGravityGravityTouchPreDelegate(ref CTriggerGravityGravityTouchPreContext ctx);
public delegate void OnCTriggerGravityGravityTouchPostDelegate(ref CTriggerGravityGravityTouchPostContext ctx);

public interface ICTriggerGravityGravityTouchHook
{
    public event OnCTriggerGravityGravityTouchPreDelegate Pre;
    public event OnCTriggerGravityGravityTouchPostDelegate Post;

    public void Invoke(CTriggerGravity schemaObject);
}