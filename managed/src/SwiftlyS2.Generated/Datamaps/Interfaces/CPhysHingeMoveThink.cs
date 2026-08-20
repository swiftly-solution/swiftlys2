using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysHingeMoveThinkPreContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysHingeMoveThinkPostContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysHingeMoveThinkPreDelegate(ref CPhysHingeMoveThinkPreContext ctx);
public delegate void OnCPhysHingeMoveThinkPostDelegate(ref CPhysHingeMoveThinkPostContext ctx);

public interface ICPhysHingeMoveThinkHook
{
    public event OnCPhysHingeMoveThinkPreDelegate Pre;
    public event OnCPhysHingeMoveThinkPostDelegate Post;

    public void Invoke(CPhysHinge schemaObject);
}