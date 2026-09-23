using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPathMoverEntitySpawner : IGameHookDatamapCPathMoverEntitySpawner
{
    internal readonly CPathMoverEntitySpawnerDebugThinkHook CPathMoverEntitySpawnerDebugThinkHook = new();
    internal readonly CPathMoverEntitySpawnerSpawnThinkHook CPathMoverEntitySpawnerSpawnThinkHook = new();

    public ICPathMoverEntitySpawnerDebugThinkHook DebugThink => CPathMoverEntitySpawnerDebugThinkHook;
    public ICPathMoverEntitySpawnerSpawnThinkHook SpawnThink => CPathMoverEntitySpawnerSpawnThinkHook;

    internal void UnregisterListeners()
    {
        CPathMoverEntitySpawnerDebugThinkHook.UnregisterListeners();
        CPathMoverEntitySpawnerSpawnThinkHook.UnregisterListeners();
    }
}