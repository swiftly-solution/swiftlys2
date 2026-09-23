using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPathMoverEntitySpawnerDebugThinkPreContext
{
    public CPathMoverEntitySpawner SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPathMoverEntitySpawnerDebugThinkPostContext
{
    public CPathMoverEntitySpawner SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPathMoverEntitySpawnerDebugThinkPreDelegate(ref CPathMoverEntitySpawnerDebugThinkPreContext ctx);
public delegate void OnCPathMoverEntitySpawnerDebugThinkPostDelegate(ref CPathMoverEntitySpawnerDebugThinkPostContext ctx);

public interface ICPathMoverEntitySpawnerDebugThinkHook
{
    public event OnCPathMoverEntitySpawnerDebugThinkPreDelegate Pre;
    public event OnCPathMoverEntitySpawnerDebugThinkPostDelegate Post;

    public void Invoke(CPathMoverEntitySpawner schemaObject);
}