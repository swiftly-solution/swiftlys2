using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysHingeSoundThinkPreContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysHingeSoundThinkPostContext
{
    public CPhysHinge SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysHingeSoundThinkPreDelegate(ref CPhysHingeSoundThinkPreContext ctx);
public delegate void OnCPhysHingeSoundThinkPostDelegate(ref CPhysHingeSoundThinkPostContext ctx);

public interface ICPhysHingeSoundThinkHook
{
    public event OnCPhysHingeSoundThinkPreDelegate Pre;
    public event OnCPhysHingeSoundThinkPostDelegate Post;

    public void Invoke(CPhysHinge schemaObject);
}