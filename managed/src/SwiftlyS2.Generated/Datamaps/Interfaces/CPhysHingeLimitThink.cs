using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysHingeLimitThinkPreContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysHingeLimitThinkPostContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysHingeLimitThinkPreDelegate(ref CPhysHingeLimitThinkPreContext ctx);
public delegate void OnCPhysHingeLimitThinkPostDelegate(ref CPhysHingeLimitThinkPostContext ctx);

public interface ICPhysHingeLimitThinkHook
{
    public event OnCPhysHingeLimitThinkPreDelegate Pre;
    public event OnCPhysHingeLimitThinkPostDelegate Post;

    public void Invoke(CPhysHinge schemaObject);
}