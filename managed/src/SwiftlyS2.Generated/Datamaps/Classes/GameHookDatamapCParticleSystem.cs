using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCParticleSystem : IGameHookDatamapCParticleSystem
{
    internal readonly CParticleSystemStartParticleSystemThinkHook CParticleSystemStartParticleSystemThinkHook = new();

    public ICParticleSystemStartParticleSystemThinkHook StartParticleSystemThink => CParticleSystemStartParticleSystemThinkHook;

    internal void UnregisterListeners()
    {
        CParticleSystemStartParticleSystemThinkHook.UnregisterListeners();
    }
}