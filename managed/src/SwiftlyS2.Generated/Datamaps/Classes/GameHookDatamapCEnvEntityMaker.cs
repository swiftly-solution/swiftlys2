using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvEntityMaker(GameHooksService owner) : IGameHookDatamapCEnvEntityMaker
{
    internal readonly CEnvEntityMakerCheckSpawnThinkHook CEnvEntityMakerCheckSpawnThinkHook = new(owner);

    public ICEnvEntityMakerCheckSpawnThinkHook CheckSpawnThink => CEnvEntityMakerCheckSpawnThinkHook;

    internal void UnregisterListeners()
    {
        CEnvEntityMakerCheckSpawnThinkHook.UnregisterListeners();
    }
}