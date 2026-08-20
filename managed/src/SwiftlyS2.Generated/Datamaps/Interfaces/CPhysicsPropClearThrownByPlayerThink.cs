using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicsPropClearThrownByPlayerThinkPreContext
{
    public CPhysicsProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicsPropClearThrownByPlayerThinkPostContext
{
    public CPhysicsProp SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicsPropClearThrownByPlayerThinkPreDelegate(ref CPhysicsPropClearThrownByPlayerThinkPreContext ctx);
public delegate void OnCPhysicsPropClearThrownByPlayerThinkPostDelegate(ref CPhysicsPropClearThrownByPlayerThinkPostContext ctx);

public interface ICPhysicsPropClearThrownByPlayerThinkHook
{
    public event OnCPhysicsPropClearThrownByPlayerThinkPreDelegate Pre;
    public event OnCPhysicsPropClearThrownByPlayerThinkPostDelegate Post;

    public void Invoke(CPhysicsProp schemaObject);
}