using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCParticleSystem(GameHooksService owner) : IGameHookDatamapCParticleSystem
{
    internal readonly CParticleSystemStartParticleSystemThinkHook CParticleSystemStartParticleSystemThinkHook = new(owner);

    public ICParticleSystemStartParticleSystemThinkHook StartParticleSystemThink => CParticleSystemStartParticleSystemThinkHook;

    internal void UnregisterListeners()
    {
        CParticleSystemStartParticleSystemThinkHook.UnregisterListeners();
    }
}