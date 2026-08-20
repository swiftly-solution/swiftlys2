using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvBeam : IGameHookDatamapCEnvBeam
{
    internal readonly CEnvBeamStrikeThinkHook CEnvBeamStrikeThinkHook = new();
    internal readonly CEnvBeamUpdateThinkHook CEnvBeamUpdateThinkHook = new();

    public ICEnvBeamStrikeThinkHook StrikeThink => CEnvBeamStrikeThinkHook;
    public ICEnvBeamUpdateThinkHook UpdateThink => CEnvBeamUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CEnvBeamStrikeThinkHook.UnregisterListeners();
        CEnvBeamUpdateThinkHook.UnregisterListeners();
    }
}