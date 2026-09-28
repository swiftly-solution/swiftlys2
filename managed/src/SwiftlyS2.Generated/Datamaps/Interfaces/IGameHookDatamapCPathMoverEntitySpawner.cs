namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPathMoverEntitySpawner
{
    public ICPathMoverEntitySpawnerDebugThinkHook DebugThink { get; }
    public ICPathMoverEntitySpawnerSpawnThinkHook SpawnThink { get; }
}