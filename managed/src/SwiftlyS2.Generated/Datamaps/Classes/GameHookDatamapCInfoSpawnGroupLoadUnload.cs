using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCInfoSpawnGroupLoadUnload(GameHooksService owner) : IGameHookDatamapCInfoSpawnGroupLoadUnload
{
    internal readonly CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook = new(owner);
    internal readonly CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook = new(owner);

    public ICInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook SpawnGroupLoadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook;
    public ICInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook SpawnGroupUnloadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook;

    internal void UnregisterListeners()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook.UnregisterListeners();
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook.UnregisterListeners();
    }
}