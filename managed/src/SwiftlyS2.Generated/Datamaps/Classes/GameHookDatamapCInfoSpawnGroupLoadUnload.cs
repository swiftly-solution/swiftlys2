using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCInfoSpawnGroupLoadUnload : IGameHookDatamapCInfoSpawnGroupLoadUnload
{
    internal readonly CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook = new();
    internal readonly CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook = new();

    public ICInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook SpawnGroupLoadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook;
    public ICInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook SpawnGroupUnloadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook;

    internal void UnregisterListeners()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.UnregisterListeners();
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.UnregisterListeners();
    }
}