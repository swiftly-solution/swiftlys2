using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicsPropRespawnableMaterializePreContext
{
    public CPhysicsPropRespawnable SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicsPropRespawnableMaterializePostContext
{
    public CPhysicsPropRespawnable SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicsPropRespawnableMaterializePreDelegate(ref CPhysicsPropRespawnableMaterializePreContext ctx);
public delegate void OnCPhysicsPropRespawnableMaterializePostDelegate(ref CPhysicsPropRespawnableMaterializePostContext ctx);

public interface ICPhysicsPropRespawnableMaterializeHook
{
    public event OnCPhysicsPropRespawnableMaterializePreDelegate Pre;
    public event OnCPhysicsPropRespawnableMaterializePostDelegate Post;

    public void Invoke(CPhysicsPropRespawnable schemaObject);
}