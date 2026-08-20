using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysForceForceOffPreContext
{
    public CPhysForce SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysForceForceOffPostContext
{
    public CPhysForce SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysForceForceOffPreDelegate(ref CPhysForceForceOffPreContext ctx);
public delegate void OnCPhysForceForceOffPostDelegate(ref CPhysForceForceOffPostContext ctx);

public interface ICPhysForceForceOffHook
{
    public event OnCPhysForceForceOffPreDelegate Pre;
    public event OnCPhysForceForceOffPostDelegate Post;

    public void Invoke(CPhysForce schemaObject);
}