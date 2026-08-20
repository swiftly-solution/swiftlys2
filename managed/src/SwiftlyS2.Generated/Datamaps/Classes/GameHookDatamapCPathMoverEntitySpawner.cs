using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPathMoverEntitySpawner : IGameHookDatamapCPathMoverEntitySpawner
{
    internal readonly CPathMoverEntitySpawnerSpawnThinkHook CPathMoverEntitySpawnerSpawnThinkHook = new();

    public ICPathMoverEntitySpawnerSpawnThinkHook SpawnThink => CPathMoverEntitySpawnerSpawnThinkHook;

    internal void UnregisterListeners()
    {
        CPathMoverEntitySpawnerSpawnThinkHook.UnregisterListeners();
    }
}