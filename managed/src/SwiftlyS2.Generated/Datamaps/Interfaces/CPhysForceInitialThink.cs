using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysForceInitialThinkPreContext
{
    public CPhysForce SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysForceInitialThinkPostContext
{
    public CPhysForce SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysForceInitialThinkPreDelegate(ref CPhysForceInitialThinkPreContext ctx);
public delegate void OnCPhysForceInitialThinkPostDelegate(ref CPhysForceInitialThinkPostContext ctx);

public interface ICPhysForceInitialThinkHook
{
    public event OnCPhysForceInitialThinkPreDelegate Pre;
    public event OnCPhysForceInitialThinkPostDelegate Post;

    public void Invoke(CPhysForce schemaObject);
}