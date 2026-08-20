using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPathMoverEntitySpawnerSpawnThinkPreContext
{
    public CPathMoverEntitySpawner SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPathMoverEntitySpawnerSpawnThinkPostContext
{
    public CPathMoverEntitySpawner SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPathMoverEntitySpawnerSpawnThinkPreDelegate(ref CPathMoverEntitySpawnerSpawnThinkPreContext ctx);
public delegate void OnCPathMoverEntitySpawnerSpawnThinkPostDelegate(ref CPathMoverEntitySpawnerSpawnThinkPostContext ctx);

public interface ICPathMoverEntitySpawnerSpawnThinkHook
{
    public event OnCPathMoverEntitySpawnerSpawnThinkPreDelegate Pre;
    public event OnCPathMoverEntitySpawnerSpawnThinkPostDelegate Post;

    public void Invoke(CPathMoverEntitySpawner schemaObject);
}