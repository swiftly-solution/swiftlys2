using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicsPropClearFlagsThinkPreContext
{
    public CPhysicsProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicsPropClearFlagsThinkPostContext
{
    public CPhysicsProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicsPropClearFlagsThinkPreDelegate(ref CPhysicsPropClearFlagsThinkPreContext ctx);
public delegate void OnCPhysicsPropClearFlagsThinkPostDelegate(ref CPhysicsPropClearFlagsThinkPostContext ctx);

public interface ICPhysicsPropClearFlagsThinkHook
{
    public event OnCPhysicsPropClearFlagsThinkPreDelegate Pre;
    public event OnCPhysicsPropClearFlagsThinkPostDelegate Post;

    public void Invoke(CPhysicsProp schemaObject);
}