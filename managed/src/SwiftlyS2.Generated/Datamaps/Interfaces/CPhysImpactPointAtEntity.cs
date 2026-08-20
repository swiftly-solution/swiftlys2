using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysImpactPointAtEntityPreContext
{
    public CPhysImpact SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysImpactPointAtEntityPostContext
{
    public CPhysImpact SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysImpactPointAtEntityPreDelegate(ref CPhysImpactPointAtEntityPreContext ctx);
public delegate void OnCPhysImpactPointAtEntityPostDelegate(ref CPhysImpactPointAtEntityPostContext ctx);

public interface ICPhysImpactPointAtEntityHook
{
    public event OnCPhysImpactPointAtEntityPreDelegate Pre;
    public event OnCPhysImpactPointAtEntityPostDelegate Post;

    public void Invoke(CPhysImpact schemaObject);
}