using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonPhysicsThinkPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonPhysicsThinkPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonPhysicsThinkPreDelegate(ref CPhysicalButtonPhysicsThinkPreContext ctx);
public delegate void OnCPhysicalButtonPhysicsThinkPostDelegate(ref CPhysicalButtonPhysicsThinkPostContext ctx);

public interface ICPhysicalButtonPhysicsThinkHook
{
    public event OnCPhysicalButtonPhysicsThinkPreDelegate Pre;
    public event OnCPhysicalButtonPhysicsThinkPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}