namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCInfoSpawnGroupLoadUnload
{
    public ICInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook SpawnGroupLoadingThink { get; }
    public ICInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook SpawnGroupUnloadingThink { get; }
}