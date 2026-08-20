using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvEntityMaker : IGameHookDatamapCEnvEntityMaker
{
    internal readonly CEnvEntityMakerCheckSpawnThinkHook CEnvEntityMakerCheckSpawnThinkHook = new();

    public ICEnvEntityMakerCheckSpawnThinkHook CheckSpawnThink => CEnvEntityMakerCheckSpawnThinkHook;

    internal void UnregisterListeners()
    {
        CEnvEntityMakerCheckSpawnThinkHook.UnregisterListeners();
    }
}